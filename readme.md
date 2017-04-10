# Platform Easy Settings

## Overview
This will load the configuration from the consul cluster based on named conventions
The settings will also aid in the service discovery for the application

## Usage
you can load the configuration used within a dotnet core app

### Installing 
you can install through nuget 
```
dotnet add package easy-settings
```

### Adding to your project
Once the nuget package is referenced you can start using easy-settings in your project. 
You can start by loading the json file

include the easy-settings into your project
```
using EasySettings;
```

Then load the configuration files for your application
```
var loader = new EasySettings.Configuration();
var configuration = loader.Load();
```

this will have returned the IConfiguration as defined in the Microsoft.Extensions.Configuration 