# Library database

Console based application for managing simple library database.

## Features

1. Inserting, delete, print and update data to 5 different tables.
2. Importing data from Json file to 2 different tables.
3. Clearing all tables in database.

## Getting Started

### Dependencies

<ul>
    <li>.NET SDK latest recommended</li>
    <li>SQL Database for data storage</li>
    <li>
        NuGet packages:
        <ul>
            <li>System.Data.SqlClient</li>
            <li>System.Configuration.ConfigurationManager</li>
            <li>Newtonsoft.Json</li>
        </ul>
    </li>
</ul>

### Installing

1. Open Visual Studio.
2. Click Clone a repository.
3. Paste https://github.com/Forkxel/Database-project.git to Repository location.
4. In Solution Explorer find App.config and open it.
5. In the file you will find
<ul>
    <ul>
        <li>DataSource - Enter name of your server.</li>
        <li>Database - Enter name of your database.</li>
        <li>Name - Your user name.</li>
        <li>Password - Your password.</li>
    </ul>
</ul>

```
<add key="DataSource" value=""/>
<add key="Database" value=""/>
<add key="Name" value=""/>
<add key="Password" value=""/>
```
6. Install NuGet packages if needed.
7. Run the application.

### Executing program

First thing you will have to do after running application is to choose if you want to:
<ol>
    <li><a href="#1-insert-data">Insert data</a></li>
    <li><a href="#2-delete-data">Delete data</a></li>
    <li><a href="#3-print-data">Print data</a></li>
    <li><a href="#4-update-data">Update data</a></li>
    <li><a href="#5-import-data-from-json-file">Import data from Json file</a></li>
    <li><a href="#6-clear-database">Clear the database</a></li>
    <li><a href="#7-exit-application">Exit application</a></li>
</ol>
 
#### 1. Insert data

If you chose to insert data you will be asked to choose to what table you will want to insert data.
After you have chosen your table the application will want from you to write data to all table columns.

#### 2. Delete data

If you chose to delete data you will have to choose from what table you want to delete. 
Then the application will print all rows in the selected table and you will need to select ID of row you will want to delete.

#### 3. Print data

Application will print all tables to the console.

#### 4. Update Data

If you chose to update data you will have to choose what table you want to update.
After that you will have to choose how many columns from the table you have selected you want to update.
And finally you will be asked to write new data to selected columns.

#### 5. Import data from Json file

To import data you will need to find directory where is this application.
In this directory open \Database project\bin\Debug\net8.0 and find and open file named import.json.

Structure of the file will look like this:

```
{
    "author" : [
        {
            "firstName" : "exampleFirstName",
            "lastName" : "exampleLastName"
        }
    ],

    "category" : [
        {
            "name" : "exampleName"
        }
    ]
}
```
To import your custom authors and categories just copy for example from author 
```
{   
    "firstName" : "exampleFirstName",
    "lastName" : "exampleLastName"
}
```
And paste it under first author and don't forget to put , behind your last author.
Now change exampleFirstName to name you want and do the same with exampleLastName.
If you done everything correct you will end up with this:
```
"author" : [
        {
            "firstName" : "exampleFirstName",
            "lastName" : "exampleLastName"
        },
        {
            "firstName" : "exampleFirstName",
            "lastName" : "exampleLastName"
        }
    ],
```
For category do exactly the same just with "name"

Don't change "firstName", "lastName" in author and "name" in category!!!

#### 6. Clear database

After you typed clear you will be asked second time if you are really sure you want to clear your database.
Now the database will clear all tables.

#### 7. Exit application

I hope I don't have to explain this :).


## Help

If you do not use MSSQL server, this app might not work.

If you need anything from me about this application contact me at:
* pavel.halik06@gmail.com
