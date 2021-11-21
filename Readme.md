# PdfInJsonApi

A projet to evaluate if response compression would improve the performance when sending a pdf as a base64 string inside a json.

### Conclusion

The performance are worst when compressing the response.

## Databse

To start the database, you need to have docker on your system and run:

```
.\oracle-database.ps1
```

## Insert data into the database

To insert a bunch of pdf into the database, you need to run will the api is running

```
.\insert-test-data.ps1
```

## Run k6 script

To run k6 test, go into the k6 directory and run the script you want

```
k6 run .\script-avec-compression.js
k6 run .\script-metadata-and-stream.js
k6 run .\script-sans-compression.js
```

## Migrations

The database schema is manage using entity framework code first.

> You can skip this section if you don't need to change the database schema. It will be apply automaically at startup

### Install dotnet ef

If you havn't already install the tools

```
dotnet tool install --global dotnet-ef
```

### Update dotnet ef

```
dotnet tool update --global dotnet-ef
```

### Create a migration

The prompt must ba inside the PdfInJsonApi folder that contains the csproj.

```
cd PdfInJsonApi
dotnet ef --startup-project . migrations add InitialSchema
```

New migrations are apply automatically at startup.

> To be compatible with oracle xe-11 you may need to manually edit the generated code.

### Remove all the migrations

To start from zero, you can run

```
dotnet ef migrations remove
```
