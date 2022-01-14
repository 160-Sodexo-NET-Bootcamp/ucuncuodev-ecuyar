# Trash Management System API (updated) ⚡

This project is third homework that made for Sodexo .NET Bootcamp and also an update of the [second homework.](https://github.com/160-Sodexo-NET-Bootcamp/ikinci-hafta-odevi-ecuyar)

You can check the project's scope from the link.

## Updates

### ✔️ Manual DTO Mapping -> AutoMapper

System doesn't use manual mapping, it uses AutoMapper package for converting data models to dtos.


### ✔️ Added middleware to handle a request

In `Vehicle` controller, we created an API point to get vehicle by id number but request is blocked by a `403 Forbidden` message in the middleware.

You can test this by sending a `GET` request to `https://localhost:44384/api/Vehicles/getVehicleById?vehicleId=[id]`. Give a `vehicleId` as a query parameter.
