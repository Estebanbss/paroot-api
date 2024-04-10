# Paroot API

## Description

This API allows you to create and manage shortened URLs. It provides endpoints to create new short URLs, retrieve existing URLs by ID or original URL, redirect to the original URL from the short URL, update existing URLs, and delete URLs. 

## Technologies

- ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
- ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
- ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
- Entity Framework Core

## Endpoints

### Create Short URL
- **URL**: `POST /`
- **Description**: Create a new short URL.
- **Request Body**: JSON object containing the original URL.
- **Response**: Newly created short URL.

### Get All URLs
- **URL**: `GET /all`
- **Description**: Get all existing short URLs.
- **Response**: List of all short URLs.

### Get URL by ID
- **URL**: `GET /{id}`
- **Description**: Get a short URL by its ID.
- **Response**: Short URL object.

### Get URL by Original URL
- **URL**: `GET /OriginalUrl/{originalUrl}`
- **Description**: Get a short URL by its original URL.
- **Response**: Short URL object.

### Redirect to Original URL
- **URL**: `GET /{shortUrl}`
- **Description**: Redirect to the original URL associated with the short URL.
- **Response**: Redirect to the original URL.

### Update URL
- **URL**: `PUT /{id}`
- **Description**: Update an existing short URL by its ID.
- **Request Body**: JSON object containing the updated URL details.
- **Response**: No content.

### Delete URL
- **URL**: `DELETE /{id}`
- **Description**: Delete a short URL by its ID.
- **Response**: No content.

## Getting Started

To get started with the Paroot API, follow these steps:

1. Clone this repository to your local machine.
2. Restore dependencies using `dotnet restore`.
3. Run the application using `dotnet run`.
4. Use the provided endpoints to interact with the API.

## Contact

If you have any questions or feedback, feel free to contact us at [juanesbs2003@hotmail.com](mailto:juanesbs2003@hotmail.com).

## Additional Information

This API is designed to provide URL shortening functionality and can be integrated into various applications where such functionality is required.
