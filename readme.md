# ASP NET Core with VueJS 3 Composition API (Quasar Framework) ECommerce Project

## Features
- Entity Framework Core – Code First 
- Response Wrappers
- Action Filters
- Pagination,Search,Filter
- Net Core Identity with JWT Authentication,Refresh Token
- Role Based Authorization
- Database Seeding
- Custom Exception Handling Middleware
- Complete User Management  (Register / Generate Token / Forgot Password / Confirmation Mail)
- Logging (Serilog),Caching (Memory,Redis),Validation (Fluent Validation),Transaction,Exception,Performance with Aspects (Autofac,Castle.DynamicProxy)

## How To Start Asp Net Core API

For asp net core, you must edit the appsettings.json file before typing these commands. 

```sh
dotnet ef migrations add CreateDatabase --project "DataAccess" --startup-project "WebAPI"
dotnet ef database update --project "DataAccess" --startup-project "WebAPI"
```
After these commands, a database will be created. 
Default Admin Account : 

```sh
Username : admin
Password : 159357456qW
```

Checkout Page Credit Card Information : 

```sh
CardName : "John Doe"
CardName : "5528790000000008"
ExpirationMonth : "12"
ExpirationYear : "2030"
Cvc : "123"
```

## How To Start Quasar Project

Project requires [Quasar Framework](https://quasar.dev) 


```sh
npm install
quasar dev
```



