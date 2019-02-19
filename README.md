# Random user Web API
Free API in .Net Core to exercise the coding of a RESTful architectural style API.

![Net.Core](https://img.shields.io/badge/net--core-2.2-blue.svg)

## How It Works

This .Net Core Web Api returns simple JSON/hash responses from the Random User Generator API. The service consume the data from [RandomUser.me](https://randomuser.me) and saves it in a temporal database in-memory. Later the data is available for consume  

## Configuration 

The project requires .NET Core 2.2 o later, Visual Studio & Entity Framework.

1. Open the project
1. Open the solution properties page
   1. Select `Multiple startup projects`.
1. Expand `Random.User.Rest`
   1. Open `appsettings.json`
   1. Set `MaxUsersLimit` to 50
   1. Set `GeneratorSiteURI` to "https://randomuser.me/api/?results=500"
1. Verify the Port number inside the `Random.User.Rest` properties page, tab `debug`. This port will be used to config the ApiURI in the Web site project. 
1. Expand `Random.User.Web.Site`
   1. Open `appsettings.json`
   1. Set `ApiURI` to "https://localhost:[PORT]/api/" >
   _Replace "PORT" with the port from the Rest project_

## Usage 
_Replace PORT with your configured port_

* GET all users: https://localhost:[PORT]/api/user
* GET users with pagination: https://localhost:[PORT]/api/user?page=X
   * _Replace X with page
* GET users paginated and limited to NN registers: https://localhost:[PORT]/api/user?page=X&cant=NN 
   * _Replace X with page and NN with the registers
* POST a new user (insert): https://localhost:[PORT]/api/user
   * `{
       "gender": "male",
       "name": "madame gilberte garnier",
       "email": "gilberte.garnier@example.com",
       "birthDate": "1965-12-30T16:23:42Z",
       "uuid": "931729ea-86c9-4db3-b633-9c3c553349f5",
       "userName": "whiteelephant926",
       "location": null
      }`
* PUT a user (edit): https://localhost:[PORT]/api/user/id
   * _Replace *ID* with the desired ID to edit_
   * `{
       "userId": 87,
       "gender": "male",
       "name": "madame gilberte garnier",
       "email": "gilberte.garnier@example.com",
       "birthDate": "1965-12-30T16:23:42Z",
       "uuid": "931729ea-86c9-4db3-b633-9c3c553349f5",
       "userName": "whiteelephant926",
       "location": null
      }`
* DELETE a user: https://localhost:[PORT]/api/user/id
   * _Replace *ID* with the desired ID to delete_

## Notes

Remember, more is not always better!

## Contributing
This project is not open for contributing.

#### Pending Features
- Better sorting
- Functional CRUDS on the Web site
