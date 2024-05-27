### Logging Project

This project demonstrates the use of various logging libraries in .NET, including Microsoft.Extensions.Logging, Serilog, and NLog. It includes examples of logging to a file, database, and console.

## Table of Contents
- [Description](#description)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
 - [Serilog](#serilog)
 - [NLog](#nlog)
- [Console Logging](#console-logging)
- [File Logging](#file-logging)
- [Database Logging](#database-logging)
- [License](#license)

## Description

This project demonstrates various approaches to logging in .NET using:
- **Microsoft.Extensions.Logging**
- **Serilog**
- **NLog**

The project includes examples of logging to a file, database, and console using each provider.

## Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/your-repository.git
   cd your-repository` 

2.  Install the necessary packages via NuGet:
    
    sh
    
    Copy code
    
    `dotnet restore` 
    

## Usage

Run the project:

sh

Copy code

`dotnet run` 

## Configuration

### Serilog

Serilog configuration is done in `appsettings.json`:

json

Copy code

`{
  "Serilog": {
    "MinimumLevel": "Debug",
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "Data/Logs/log.txt"
        }
      },
      {
        "Name": "PostgreSQL",
        "Args": {
          "connectionString": "Server=localhost;Port=5432;Database=logg_db;User Id=postgres;Password=123456",
          "tableName": "Logs"
        }
      }
    ]
  }
}` 

### NLog

NLog configuration is done in `nlog.config.json`:

json

Copy code

`{
  "NLog": {
    "autoReload": true,
    "throwConfigExceptions": true,
    "internalLogLevel": "warn",
    "internalLogFile": "internal-nlog.txt",
    "targets": {
      "console": {
        "type": "Console"
      },
      "file": {
        "type": "File",
        "fileName": "Data/Logs/log.txt",
        "layout": "${longdate}|${level:uppercase=true}|${logger}|${message}|${exception}"
      },
      "database": {
        "type": "Database",
        "connectionString": "Server=localhost;Port=5432;Database=logg_db;User Id=postgres;Password=123456",
        "commandText": "INSERT INTO Logs (Date, Level, Message, Exception) VALUES (@time_stamp, @level, @message, @exception)",
        "parameters": [
          {
            "name": "@time_stamp",
            "layout": "${longdate}"
          },
          {
            "name": "@level",
            "layout": "${level}"
          },
          {
            "name": "@message",
            "layout": "${message}"
          },
          {
            "name": "@exception",
            "layout": "${exception:format=tostring}"
          }
        ]
      }
    },
    "rules": [
      {
        "logger": "*",
        "minLevel": "Debug",
        "writeTo": "console,file,database"
      }
    ]
  }
}` 

## Console Logging

Console logging is done using the `Debug` and `Console` providers:

csharp

Copy code

`builder.Logging.ClearProviders();
builder.Logging.AddDebug();
builder.Logging.AddConsole();` 

## File Logging

File logging is configured in `appsettings.json` for Serilog and in `nlog.config.json` for NLog.

### Serilog Example

json

Copy code

`{
  "Name": "File",
  "Args": {
    "path": "Data/Logs/log.txt"
  }
}` 

### NLog Example

json

Copy code

`{
  "file": {
    "type": "File",
    "fileName": "Data/Logs/log.txt",
    "layout": "${longdate}|${level:uppercase=true}|${logger}|${message}|${exception}"
  }
}` 

## Database Logging

Database logging is also configured in `appsettings.json` for Serilog and in `nlog.config.json` for NLog.

### Serilog Example

json

Copy code

`{
  "Name": "PostgreSQL",
  "Args": {
    "connectionString": "Server=localhost;Port=5432;Database=logg_db;User Id=postgres;Password=123456",
    "tableName": "Logs"
  }
}` 

### NLog Example

json

Copy code

`{
  "database": {
    "type": "Database",
    "connectionString": "Server=localhost;Port=5432;Database=logg_db;User Id=postgres;Password=123456",
    "commandText": "INSERT INTO Logs (Date, Level, Message, Exception) VALUES (@time_stamp, @level, @message, @exception)",
    "parameters": [
      {
        "name": "@time_stamp",
        "layout": "${longdate}"
      },
      {
        "name": "@level",
        "layout": "${level}"
      },
      {
        "name": "@message",
        "layout": "${message}"
      },
      {
        "name": "@exception",
        "layout": "${exception:format=tostring}"
      }
    ]
  }
}`
