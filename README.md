# Cat Fact Application

A simple C#/.NET console application that retrieves a random cat fact from the Cat Fact API and appends the received data to a local text file.

## Technologies

* C#
* .NET
* `HttpClient`
* `System.Text.Json`
* Async/Await
* Dependency Injection ready architecture

## How it works

The application follows a simple separation of responsibilities:

```text
Program
   ↓
ApplicationService
   ↓
IApiClient → Cat Fact API
   ↓
IFileWriter → result.txt
```

### `ApiClient`

Responsible for communicating with the external API and deserializing the response into a `CatFact` object.

### `ApplicationService`

Coordinates the application flow:

1. Requests a cat fact from the API.
2. Passes the received data to the file writer.

### `FileWriter`

Serializes the received data to JSON and appends it as a new line to `result.txt`.

The file is created automatically if it does not exist.

## Example output

After several requests, `result.txt` may contain:

```text
{"Fact":"Jaguars are the only big cats that don't roar.","Length":46}
{"Fact":"The cat who holds the record for the longest non-fatal fall is Andy.","Length":157}
```

Each request adds a new line without overwriting previously saved data.

## Error handling

The application handles common errors such as:

* HTTP/API errors
* File I/O errors
* Unexpected exceptions

Errors are reported in the console.

## Running the application

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Build the project.
5. Run the application.

The application will request a cat fact and append the result to `result.txt`.

## Project structure

```text
NetWise/
├── .gitattributes
├── .gitignore
├── README.md
└── RecruitmentTask/
    ├── RecruitmentTask.sln
    ├── RecruitmentTask.csproj
    ├── Program.cs
    ├── Client/
    │   ├── IApiClient.cs
    │   └── ApiClient.cs
    ├── File/
    │   ├── IFileWriter.cs
    │   └── FileWriter.cs
    ├── Model/
    │   └── CatFact.cs
    └── Service/
        └── ApplicationService.cs
```

## API

The application uses the [Cat Fact API](https://catfact.ninja/) to retrieve random cat facts.
