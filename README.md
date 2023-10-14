# OryDotNet

> This project is currently under early development.

OryDotNet provides user interfaces for the [Ory](https://www.ory.sh/) APIs. Ory is a 
[FOSS solution](https://www.ory.sh/open-source/) for authentication, authorization, access control, and delegation.

The Ory stack consists of the following services:
1. [Ory Kratos](https://www.ory.sh/kratos/): "Cloud native user management system. Provision IDs, store user 
information, configure authentication methods and use a headless API."
2. [Ory Hydra](https://www.ory.sh/hydra/): "Cloud native user management system. Provision IDs, store user 
information, configure authentication methods and use a headless API."
3. [Ory OathKeeper](https://www.ory.sh/oathkeeper/): "Cloud native user management system. Provision IDs, store 
user information, configure authentication methods and use a headless API."
4. [Ory Keto](https://www.ory.sh/keto/): "Authorization Server inspired by Google's consistent, global 
Authorization System, providing granular access policies with RBAC, ABAC and ACL."

![GitHub last commit](https://img.shields.io/github/last-commit/josxha/OryDotNet)
![GitHub issues](https://img.shields.io/github/issues/josxha/OryDotNet)
![GitHub Repo stars](https://img.shields.io/github/stars/josxha/OryDotNet?style=social)

## State of development

This is the current development state:

### Ory Admin UI
OryAdmin is intended to be an administrative interface for the Ory services. It is inspired by 
the [kratos-admin-ui](https://github.com/dfoxg/kratos-admin-ui) project.
- [x] View Identities
- [x] View Identity with active sessions
- [x] Delete Identity
- [x] Update Password / Account Recovery
- [x] Create Identity
- [ ] Edit Identity

### Ory Kratos Self Service UI
KratosSelfService aims to be a drop-in replacement for the [kratos-selfservice-ui-node](https://github.com/ory/kratos-selfservice-ui-node).

- [x] Login
- [x] Registration
- [x] Verify Email
- [ ] Account Settings
- [x] Logout
- [ ] Error Page
- [ ] Recovery
- [x] Sessions

## Get Started
1. Clone the project
```bash
git clone https://github.com/josxha/OryDotNet.git
```
2. Run Ory locally by running the [/ory/docker-compose.yml](ory/docker-compose.yml) file. Ory will store its data 
persistently in SqLite databases. 
```bash
cd ./ory
docker compose up -d 
```
3. Start the UI
```bash
dotnet run ./OryAdmin
dotnet run ./KratosSelfService
```