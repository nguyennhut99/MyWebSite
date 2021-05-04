# Overview Project

## Backend Site
this is API web app, build by ASP .NET Core, entity framework core, identityserver4

responsible:  communication between Customer Site and database

## Customer Site

this is MVC web app ,Build by ASP .NET Core

Features:
- display product list.
- allowed users add product to cart
- order products

## Admin Site
this is React app.
Features:
- Product Management
- Category Management
- User Management

## Installation

### install backend site

```bash
cd .\MyShop.Backend
dotnet build
```
- install database
```bash
dotnet tool install --global dotnet-ef --version 3.0.0
dotnet ef database update
```
- run app
```bash
dotnet run
```

### install customer site

```bash
cd .\CustomerSite
dotnet build
dotnet run
```
### install admin site
```bash
cd .\adminsite
npm i
npm start
```
### website demo
https://backenda5a642c4d1424c88be8b289c173f512d.azurewebsites.net/
https://customersite5620b9523a404879a94fe12805c303b8.azurewebsites.net/
https://sa132f971af2a046fa87ed12.z23.web.core.windows.net/

## references

https://github.com/thiennn/myshop#for-visual-studio
