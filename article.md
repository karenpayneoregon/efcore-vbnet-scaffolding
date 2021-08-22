# EF Core 5 with VB.NET in Windows forms

Since VB.NET arrived developer common method to interact with databases has been with a [data provider](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/data-providers) in tangent with [TableAdapter](https://docs.microsoft.com/en-us/visualstudio/data-tools/directly-access-the-database-with-a-tableadapter?view=vs-2019), 
[DataAdapter](https://docs.microsoft.com/en-us/dotnet/api/system.data.common.dataadapter?view=net-5.0) or 
using a [connection](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection?view=dotnet-plat-ext-5.0) and [command](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand?view=dotnet-plat-ext-5.0).

Then there is Entity Framework and Entity Framework Core where Entity Framework Core (`EF Core`) is currently and has been another option for interacting with databases.

EF Core makes interacting with data extremely easy although until recently there has not been a simple way to setup EF Core for VB.NET unlike C# where there are many options to setup EF Core.

**Learn how to get started with EF Core below**

# Reverse engineering

Reverse engineering is the process of scaffolding entity type classes and a DbContext class based on a database schema. It can be performed using the Scaffold-DbContext command of the EF Core Package Manager Console (PMC) tools or the dotnet ef dbcontext scaffold command of the .NET Command-line Interface ([CLI](http://example.com)) tools. In this article, the PMC will be used, not the CLI tools method.

The following shows a simple command to create a DbContext and classes to represent data in a SQL-Server database.

The database is on [SQL-Server express edition](https://www.microsoft.com/en-us/Download/details.aspx?id=101064) with a database named [NorthWind2020](https://gist.github.com/karenpayneoregon/c3361a4d4503c8851dcb43f8d6b2526f).

```
Scaffold-DbContext
    "Server=.\SQLEXPRESS;Database=NorthWind2020;Trusted_Connection=True;"
    -Provider Microsoft.EntityFrameworkCore.SqlServer
    -t "Contact","ContactType"
```

To get started, install the following [NuGet package](https://github.com/efcore/EFCore.VisualBasic) which is in alpha version.

By following Microsoft [instructions](https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli) will walkthough reverse engineering process which must be done by writting out the scaffold script then run in Visual Studio Developer PowerShell window or using dotnet ef command at the command prompt. This leaves room for errors e.g. for processing a Customer**s** table Customer is entered, an non-existing table name.

A better method is to use the [following utility](https://social.technet.microsoft.com/wiki/contents/articles/53258.windows-forms-entity-framework-core-reverse-engineering-databases.aspx?fbclid=IwAR3AJK-vxEfKLnA-9-jinLHw9MKWAggM-zqW5vobhH1za_703bGyy2sBNEU) (written in C# which does not matter, it's a windows form interface which needs any code changes). Simple read how to use in the [article](https://social.technet.microsoft.com/wiki/contents/articles/53258.windows-forms-entity-framework-core-reverse-engineering-databases.aspx?fbclid=IwAR3AJK-vxEfKLnA-9-jinLHw9MKWAggM-zqW5vobhH1za_703bGyy2sBNEU), source code is [here](https://github.com/karenpayneoregon/ScaffoldDbContextHelper).

![img](assets/scaffoldTool.png)

Once the process has completed using the utility

- `Data folder` contains a class for configuring and connecting to database tables and stored procedures.
  - The Context class (in this repository is [NorthWindConect.vb](https://github.com/karenpayneoregon/efcore-vbnet-scaffolding/blob/master/NorthWindCoreLibrary/Data/NorthWindContext.vb) sets up the database connection string with a comment to change it as it's not safe. This has been changed to store the connection string in a .json file which will be explained shortly.
  - Code in the Data.Interceptors was added after reverse engineering process.
- `Model folder` contains classes which represents tables which were reversed engineered, example [Contact.vb](https://github.com/karenpayneoregon/efcore-vbnet-scaffolding/blob/master/NorthWindCoreLibrary/Models/Contact.vb).

# Code samples

Typicaly new developers will write as much code as possible in a form then and only then will consider using classes in a single form project which is fine when there will never a need to use this code.

A better path is to separate data operations from business logic were both are separated from the user interface.

- In [DataGridViewExample](https://github.com/karenpayneoregon/efcore-vbnet-scaffolding/tree/master/DataGridViewExample) project all code is in a single project, not good for reuse.
- In [DataGridViewExample1](https://github.com/karenpayneoregon/efcore-vbnet-scaffolding/tree/master/DataGridViewExample) data operations are all in the project [NorthWindCoreLibrary](https://github.com/karenpayneoregon/efcore-vbnet-scaffolding/tree/master/NorthWindCoreLibrary) which allows for code reuse.

## Unit test

Consider writing unit test for data operations which means more code to write but allows for validating code works before using in a project as shown in [NorthWindCoreUnitTest](https://github.com/karenpayneoregon/efcore-vbnet-scaffolding/tree/master/NorthWindCoreUnitTest) project.
