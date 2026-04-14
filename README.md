# DotNet-owaspzap

A demo project showcasing how to run **DAST (Dynamic Application Security Testing)** against a small .NET web application using [OWASP ZAP](https://www.zaproxy.org/).

## Overview

This repo demonstrates how to integrate OWASP ZAP into a CI/CD pipeline to automatically scan a running .NET app for common web vulnerabilities.

## Flow

1. A minimal .NET web application is spun up as the scan target.
2. OWASP ZAP runs a DAST scan against the running app.
3. A report is generated with any findings.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (used to run the ZAP scanner)

## Usage

```bash
# Run the .NET app
dotnet run --project src/

# Run OWASP ZAP scan against the app
docker run -t ghcr.io/zaproxy/zaproxy:stable zap-baseline.py \
  -t http://localhost:5000 \
  -r zap-report.html
```

## Purpose

This project is for demo and learning purposes to show how DAST can be added to a DevSecOps pipeline for .NET applications.
