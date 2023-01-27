# _Parks API_

#### By _Robert Onstott_

#### _Parks API is a service to provide information about state and national parks in the US. Parks are searchable by the state in which they or located or by park name, using a search query. The API incorporates authentication with a JWT Bearer token, which is generated at the login page._

## Technologies Used

* _C# and .NET 6.0_
* _ASP.NET core MVC_
* _Entity Framework Core_
* _MySQL community server_
* _MySQL Workbench_
* _VS Code_
* _Github_
* _Swagger_
* _JWT Bearer_

## Description

_This is an API application that returns information about state and national parks in the US. You can call to see a list of all parks within a given state, see information about a specific park, or you may use a search query to return a park by the park's name. There are also API call methods to add, update, and delete parks from the database. Parks are associated with the states via a many-to-many relationshi, so there are also API call methods to view, update, and delete the states, as a means of organizing the park data. Parks API is built with the ASP.NET Core web development framework, utilizing Entity Framework Core for mapping C# object classes into SQL database schemas. This also maps ASP.NET controller methods onto API caller methods. For the purposes of development, the database is hosted through a local server using MySql. JWT Bearer tokens are used for authentication. They are issued upon completing the placeholder (i.e. not actually secure) login form, which will direct you to a page with your JWT, which can be copied. To test the API endpoints, use the Swagger UI. There is an "Authorize" button at the top right of the Swagger page. Click the button, enter your JWT token, and submit. The token will expire after 20 min, but during that time you are authorized to test all of the Parks API endopoints._

 -------------------------------------------------------------------------------------------------------------------------------------------------------

# General Setup

* _Download the .NET framework if you do not already have it (version 6.0 or later)_
 
  _https://dotnet.microsoft.com/en-us/download/dotnet_
  
* _Download MySQL Community Server and MySQL Workbench if you don't already have them (both from this link). Make note of both the `User ID (UID)` and the `Password (PWD)` that you define in your setup configurations for MySQL. These values will go into your `appsettings.json` file in a few steps_
  
    _https://dev.mysql.com/downloads/_
  
* _Clone this repository to your machine_

* _Open the repository and navigate to the `ParksApi` directory one level down from the root directory: `$ cd ParksApi`  and create a new file called `appsettings.json`. Copy the following code into the file, with your own values for `uid` and `pwd`_
 
 
  
  ```
      {
        "Logging": {
          "LogLevel": {
             "Default": "Information",
             "Microsoft.AspNetCore": "Warning"
          }
        },
        "AllowedHosts": "*",
        "ConnectionStrings": {
           "DefaultConnection": "Server=localhost;Port=3306;database=parks_api;uid=[Your User ID];pwd=[Your Password];"
        }
      } 

  ```  
  
* _Entity Framework Core has tools to automatically build the database schema utiliaing object mapping. While still in the lower `ParksApi` directory, enter the following command_

```
    $ dotnet ef database update   
```

* _While still in the `ParksApi` directory, enter the command `$ dotnet run`. This will start the local web server. Enter the URL https://localhost:5000 in a browser window. You are now interacting with the web app_
  
  ```
        $ dotnet run
        
        https://localhost:5000/        
  ```  
 -------------------------------------------------------------------------------------------------------------------------------------------------------
  
  # Logging In And Getting Your Token
  
 * _This will direct you to the login page. This is a dummy login that is not actually secure-- it is a placeholder in order to build out the JWT issuing    and authentication functionality. The username and secret are both the same word: `secret`._
 
 ```
  username: secret
  password: secret
 ```
 
 * _This will direct you to a "Log In Successful" page, where you will be given the text of your JWT bearer token. Copy this token to your clipboard._
 
 ![Your token is generated](./README_images/homePage_token_copy.jpg)
 
 * _From here on, there is no custom user interface for the API. To test API calls, you must use the Swagger UI or Postman. Make sure that you have the JWT token copied to your clipboard for either method:_
 
 -------------------------------------------------------------------------------------------------------------------------------------------------------
 
 # Swagger
 
 * _Enter the following URL into your browser_
 
 ```
    https://localhost:5000/swagger
 ``` 
 * _On the Swagger page, you will see tabs for all of the API endpoints, organized by the two database tables: `Parks` and `States`. Click the green `Authorize` button at the top right corner of the page._
 
![Authorizing at Swagger](./README_images/swagger_auth_1.jpg)

* _Enter the JWT that you copied before. You may now click any of the endpoints and will be authorized to make API calls.__

![Authorizing at Swagger](./README_images/swagger_auth_2.jpg)

 -------------------------------------------------------------------------------------------------------------------------------------------------------

# Postman

* _Open the Postman App and open a new tab. You will need your JWT for Postman as well. Click `Authorization` on the top bar, then select `Bearer Token` from the dropdown tab that says "Type" and then enter your token into the input field on the right_

![Authorizing at Swagger](./README_images/postman_auth.jpg)

* _You can now make API calls by selecting the controller method and entering the URL according to the endpoints outlined below. PUT and POST requests are more involved because you have to include the entire JSON body of your update to the database. For PUT and POST requests, select the controller type and enter the URL as for GET requests, then select `Body` from the toolbar above, check `Raw`, and then choose `JSON` from the dropdown list on the right. Then enter the body of your request. If it is a POST request, do not enter a ParksId or a StatesId--these are automatically generated. But if it is a PUT request, leave the ID the same_

![Authorizing at Swagger](./README_images/postman_post.jpg)
 
 -------------------------------------------------------------------------------------------------------------------------------------------------------

# API Endpoints
Base URL: `https://localhost:5000`

## HTTP Request Structure
```
GET /api/{component}
POST /api/{component}
GET /api/{component}/{id}
PUT /api/{component}/{id}
DELETE /api/{component}/{id}
```

 -------------------------------------------------------------------------------------------------------------------------------------------------------

# Parks
Access information on state and national parks in the US

## HTTP Request
```
GET /api/Parks
POST /api/Parks
GET /api/Parks/{id}
PUT /api/Parks/{id}
DELETE /api/Parks/{id}
```

## Path Parameters
| Parameter | Type | Default | Required | Description |
| :---: | :---: | :---: | :---: | --- |
| name | string | none | false | Return matches by name. Partial queries supported, so long as name contains query |
| stateName | string | none | false | Return any parks whose stateName property contains query  |

## Example Query
```
https://localhost:5000/api/Parks/?name=rain&stateName=wa

```

## Sample JSON Response
```
{
  "parkId": 3,
  "name": "Mount Rainier",
  "description": "A 14,410 active volcano with extensive (but shrinking) glacier fields. Beloved for alpine meadows carpeted in summer wildflowers",
  "stateId": 2,
  "stateName": "Washington",
  "state": null
}


* note: "state" is a navigation property for EF Core and will always be null

```

 -------------------------------------------------------------------------------------------------------------------------------------------------------

# States

## HTTP Request
```
GET /api/State
POST /api/State
GET /api/State/{id}
PUT /api/State/{id}
DELETE /api/State/{id}
```

## Path Parameters
| Parameter | Type | Default | Required | Description |
| :---: | :---: | :---: | :---: | --- |
| name | string | none | false | Return matches by name.

## Example Query
```
https://localhost:5000/api/States/?name=washington
```

## Sample JSON Response
```
{
    "stateId": 2,
    "name": "Washington",
    "parks": null
  }
  
  * note: "parks" is a navigation property and will always be null
```
 

## Known Bugs

* _No known bugs at this time_

## License

_MIT_

_Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:_

_The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software._

_THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE._

Copyright (c) January 2023_ _Robert Onstott_
