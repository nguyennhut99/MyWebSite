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

## references

https://github.com/thiennn/myshop#for-visual-studio
