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
cd SimpleApi && dotnet run --launch-profile http

# Run OWASP ZAP scan against the app
docker run -t ghcr.io/zaproxy/zaproxy:stable zap-baseline.py \
  -t http://localhost:5132 \
  -r zap-report.html
```

## API Endpoints

Base URL: `http://localhost:5132`

### Todos

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/todos` | Returns all todos |
| `GET` | `/todos/{id}` | Returns a single todo by ID |
| `POST` | `/todos` | Creates a new todo |
| `PUT` | `/todos/{id}` | Updates an existing todo by ID |
| `DELETE` | `/todos/{id}` | Deletes a todo by ID |

#### Request Body (POST / PUT)

```json
{
  "title": "Buy groceries",
  "isComplete": false
}
```

#### Example Responses

**GET /todos**
```json
[
  { "id": 1, "title": "Buy groceries", "isComplete": false },
  { "id": 2, "title": "Read a book", "isComplete": true }
]
```

**POST /todos** — returns `201 Created`
```json
{ "id": 3, "title": "Learn .NET", "isComplete": false }
```

**GET /todos/99** — returns `404 Not Found` if ID doesn't exist

## Purpose

This project is for demo and learning purposes to show how DAST can be added to a DevSecOps pipeline for .NET applications.
