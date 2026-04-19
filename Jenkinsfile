pipeline {
    agent any

    environment {
        APP_URL      = 'http://localhost:5132'
        ZAP_IMAGE    = 'ghcr.io/zaproxy/zaproxy:stable'
        REPORT_HTML  = 'zap-report.html'
        REPORT_JSON  = 'zap-report.json'
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                dir('src') {
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Start App') {
            steps {
                dir('src') {
                    sh '''
                        dotnet run --configuration Release --launch-profile http &
                        echo $! > ../app.pid
                    '''
                    sh '''
                        echo "Waiting for app to be ready..."
                        for i in $(seq 1 30); do
                            curl -sf ${APP_URL}/todos && break
                            sleep 2
                        done
                    '''
                }
            }
        }

        stage('OWASP ZAP Scan') {
            steps {
                sh '''
                    docker run --rm \
                        --network host \
                        -v ${WORKSPACE}:/zap/wrk/:rw \
                        ${ZAP_IMAGE} \
                        zap-full-scan.py \
                            -t ${APP_URL} \
                            -r ${REPORT_HTML} \
                            -J ${REPORT_JSON} \
                            -I
                '''
            }
        }
    }

    post {
        always {
            sh '''
                if [ -f app.pid ]; then
                    kill $(cat app.pid) || true
                    rm app.pid
                fi
            '''

            publishHTML(target: [
                allowMissing:          false,
                alwaysLinkToLastBuild: true,
                keepAll:               true,
                reportDir:             '.',
                reportFiles:           "${REPORT_HTML}",
                reportName:            'OWASP ZAP Report'
            ])

            archiveArtifacts artifacts: "${REPORT_HTML}, ${REPORT_JSON}",
                             allowEmptyArchive: true
        }

        failure {
            echo 'Pipeline failed — check ZAP report for findings or build errors.'
        }

        success {
            echo 'Scan complete. ZAP report published.'
        }
    }
}
