# Mars Rover Technical Challenge

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

You will need the following:

```
Visual Studio Enterprise 2017
Visual Studio Community 2017
```

### Installing

Clone down the repo

```
open package manager console and type dotnet restore --no-cache
```

```
make sure solution builds properly
```

Project defaults to the swagger UI, this will allow the user to understand the documentation of the application and how end points work and responses to expect
```
http://localhost:28080/swagger/index.html
```

The service will always have a healthcheck end point running here we can see if the service is connecting to the database successsfully
```
http://localhost:28080/v1/health
```


## Running the tests

In order to run the functional tests we need to have the service up and running. 
Build your solution make sure that the IIS is up and running 
Hit the health check, make sure the database is alive
Either open another instance of visual studio or using the command line CD to the test project folder and run the following command
```
dotnet test
```

### Break down into end to end tests

These test will make sure that each end point is inserting correct data
These tests will also make sure that the rover is moving appropriately

```
Give an example
```

### And coding style tests

The coding styles have stayed true to the RESTful best practices.
End points are clear and easy to follow


## Built With

* [Swagger](https://swagger.io/tools/swagger-ui/) - The framework used to document end points
* [VSEnterprise2017](https://visualstudio.microsoft.com/downloads/) - Visual studio 2017 donwload page


## Versioning

Current version is [V1](v1/rover)

## Authors

* **Marco A Lostaunau** - *Initial work* - [Marco A Lostaunau](https://github.com/lostaunaum)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Thank you **Claire Johnson** and **Gartner** for this opportunity
* Working on this project has been a lot of fun!
