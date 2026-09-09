# Cat Fact Application

A simple C#/.NET console application that retrieves a random cat fact from the Cat Fact API and appends the received data to a local text file.

## Technologies

* C#
* .NET 8
* `HttpClient`
* `System.Text.Json`
* Async/Await
* Dependency Injection with `Microsoft.Extensions.DependencyInjection`
* Configuration with `Microsoft.Extensions.Configuration`
* xUnit
* Moq

## How it works

The application follows a simple separation of responsibilities:

```text
Program
   ↓
ApplicationService
   ├── IApiClient
   │      ↓
   │   ApiClient
   │      ↓
   │   Cat Fact API
   │
   └── IFileWriter
          ↓
      FileWriter
          ↓
       result.txt
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

## API

The application uses the Cat Fact API:

`https://catfact.ninja/fact`

Example response:

```json
{
  "fact": "Jaguars are the only big cats that don't roar.",
  "length": 46
}
```

## Configuration

Application settings are stored in `appsettings.json`:

```json
{
  "Api": {
    "BaseUrl": "https://catfact.ninja/"
  },
  "File": {
    "ResultPath": "result.txt"
  }
}
```

The configuration is loaded using `Microsoft.Extensions.Configuration`.

`appsettings.json` is copied to the output directory when the project is built.

## Dependency Injection

Dependencies are registered in `Program.cs` using `Microsoft.Extensions.DependencyInjection`.

The application uses the following lifetimes:

* `HttpClient` — Singleton
* `IApiClient` / `ApiClient` — Singleton
* `IFileWriter` / `FileWriter` — Singleton
* `ApplicationService` — Transient

This keeps dependency creation and application composition in `Program.cs`, while the individual classes depend on abstractions.

## Error handling

The application has centralized exception handling in `Program.Main`.

The following errors are handled separately:

* `HttpRequestException` — problems with the API request
* `IOException` — problems with file operations
* `Exception` — unexpected errors

Exceptions are allowed to propagate from lower layers and are handled at the application entry point.

## Unit tests

The project contains a separate `RecruitmentTask.Tests` project using xUnit and Moq.

Currently, the main application flow is tested through `ApplicationService`.

The test mocks:

* `IApiClient` — to provide controlled test data without making a real HTTP request
* `IFileWriter` — to verify that the received data is passed to the file writer without creating a real file

This keeps the test independent from external services and the file system.

## Project structure

```text
/
├── .gitattributes
├── .gitignore
├── README.md
│
├── RecruitmentTask/
│   ├── RecruitmentTask.sln
│   ├── RecruitmentTask.csproj
│   ├── appsettings.json
│   ├── Program.cs
│   │
│   ├── Client/
│   │   ├── IApiClient.cs
│   │   └── ApiClient.cs
│   │
│   ├── File/
│   │   ├── IFileWriter.cs
│   │   └── FileWriter.cs
│   │
│   ├── Model/
│   │   └── CatFact.cs
│   │
│   └── Service/
│       └── ApplicationService.cs
│
└── RecruitmentTask.Tests/
    ├── RecruitmentTask.Tests.csproj
    └── UnitTest1.cs
```

## Running the application

1. Clone the repository.
2. Open `RecruitmentTask.sln` in Visual Studio.
3. Restore NuGet packages if necessary.
4. Build the solution.
5. Run the `RecruitmentTask` project.

The application will retrieve a cat fact and append it to `result.txt`.

## Running tests

Tests can be run from Visual Studio using **Test Explorer** or with the .NET CLI:

```bash
dotnet test
```
