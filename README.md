# DotNet-owaspzap

A demo project showcasing how to run **DAST (Dynamic Application Security Testing)** and **SAST (Static Application Security Testing)** against a small .NET web application using [OWASP ZAP](https://www.zaproxy.org/) and [Semgrep](https://semgrep.dev/).

## Overview

This repo demonstrates how to integrate security scanning into a CI/CD pipeline to automatically scan a .NET app for common web vulnerabilities. The app is intentionally vulnerable so the scans produce real findings.

## Flow

1. A minimal .NET web application is spun up as the scan target (with a static frontend at `/`).
2. OWASP ZAP runs a DAST scan against the running app — guided by an OpenAPI spec and an automation framework plan.
3. Semgrep runs a SAST scan against the source code.
4. Reports are written to the `reports/` folder.

## Prerequisites

- [.NET SDK 10.0](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) and Docker Compose

## Quick Start (Docker Compose)

The whole stack — app + ZAP — runs with a single command:

```bash
docker compose up --abort-on-container-exit
```

This builds the .NET app image, starts it, waits for it to be ready, then runs the ZAP automation plan against it. Reports land in `./reports/`.

To also run the SAST scan:

```bash
docker compose --profile sast run --rm semgrep
```

## Manual Usage

```bash
# Run the .NET app locally
cd src && dotnet run --launch-profile http

# Visit the frontend
open http://localhost:5132/

# Run OWASP ZAP full scan against the app
docker run --rm \
  --network host \
  -v $(pwd):/zap/wrk/:rw \
  ghcr.io/zaproxy/zaproxy:stable \
  zap.sh -cmd -port 8090 -autorun /zap/wrk/zap.yaml
```

## Project Layout

```
.
├── src/                       # .NET app source
│   ├── Program.cs             # Minimal API + intentional vulns
│   ├── wwwroot/index.html     # Static frontend
│   ├── Dockerfile             # Multi-stage build, runs as non-root appuser
│   └── .dockerignore
├── zap.yaml                   # ZAP Automation Framework plan (OpenAPI + auth + active scan)
├── docker-compose.yml         # app + zap + semgrep services
├── Jenkinsfile                # Pipeline: build → start app → ZAP scan → publish report
├── reports/                   # Scan output (HTML / JSON / SARIF)
└── README.md
```

## API Endpoints

Base URL: `http://localhost:5132`

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/` | Static frontend (Todo UI) |
| `GET` | `/openapi/v1.json` | OpenAPI spec (consumed by ZAP) |
| `GET` | `/todos` | Returns all todos |
| `GET` | `/todos/{id}` | Returns a single todo by ID |
| `POST` | `/todos` | Creates a new todo |
| `PUT` | `/todos/{id}` | Updates an existing todo by ID |
| `DELETE` | `/todos/{id}` | Deletes a todo by ID |
| `GET` | `/search?q=...` | Reflects user input as HTML (vuln: reflected XSS) |
| `GET` | `/debug/error` | Returns full exception detail (vuln: information disclosure) |
| `POST` | `/login` | Accepts admin/admin, sets insecure cookie (vuln: insecure session cookie) |
| `GET` | `/export?token=...` | Exports todos with token in URL (vuln: sensitive data in URL) |

#### Request Body (POST / PUT `/todos`)

```json
{ "title": "Buy groceries", "isComplete": false }
```

## Intentional Vulnerabilities

The app deliberately ships with these issues so the scans have something to find:

| Vulnerability | Where |
|---|---|
| Overly permissive CORS (`AllowAnyOrigin`) | `Program.cs` middleware |
| Server version leakage (`Server`, `X-Powered-By`, `X-AspNet-Version`) | `Program.cs` middleware |
| Missing security headers (CSP, X-Content-Type-Options, X-Frame-Options) | All responses |
| Reflected XSS | `/search` |
| Verbose error disclosure (stack trace + DB connection string) | `/debug/error` |
| Insecure session cookie (no `HttpOnly`/`Secure`/`SameSite`) | `/login` |
| Sensitive data in URL (auth token as query param) | `/export` |

## ZAP Scan Configuration

`zap.yaml` is a [ZAP Automation Framework](https://www.zaproxy.org/docs/automate/automation-framework/) plan that:

1. Imports the OpenAPI spec — so every endpoint is discovered, not just what the spider crawls.
2. Authenticates as `admin/admin` via `/login` and uses the resulting cookie for protected scans.
3. Runs both passive and active scans.
4. Writes HTML and JSON reports to `/zap/wrk/reports/` (mapped to `./reports/`).

## CI/CD

A `Jenkinsfile` is included that:

1. Checks out the code.
2. Builds the .NET app.
3. Starts the app in the background, waits up to 60s for readiness.
4. Runs `zap-full-scan.py` against the app via Docker.
5. Kills the app and publishes the HTML report via the Jenkins HTML Publisher plugin.

## Purpose

This project is for demo and learning purposes — to show how DAST and SAST can be added to a DevSecOps pipeline for .NET applications. It is intentionally vulnerable; **do not deploy it to a real environment.**
