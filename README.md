# Contact Management

This is a sample project used to demonstrate a Blazor front-end using a REST API for the back-end. It is a simple Contact Management system that only performs CRUD operations. The design of the API uses many DDD concepts that in reality are overkill for such a simple model with very little business logic. However, the design patterns were chosen to demonstrate how a more complex solution might be architected with DDD.

To run the project in Visual Studio, clone the repo and open the ContactManagement.sln solution. Configure the startup projects so that ContactManagement.Api and ContactManagement.BlazorHost are set to start.

More information about the technologies used:

*   Built with C# on .Net Core 7
*   UI is built with [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
*   Data stored in SQLite
*   Uses a [Clean Architecture](https://github.com/ardalis/CleanArchitecture) project design
*   Notable libraries used:

*   [Fast Endpoints](https://fast-endpoints.com/) are used with the [REPR pattern](https://deviq.com/design-patterns/repr-design-pattern) in place of controllers
*   [Fluent Validation](https://github.com/FluentValidation/FluentValidation)
*   [Fluent Assertions](https://github.com/fluentassertions/fluentassertions)
*   [NSubstitute](https://nsubstitute.github.io/)
*   [Ardalis.SharedKernel](https://github.com/ardalis/Ardalis.SharedKernel)

This is just a demonstration project and there is additional functionality and areas of improvement that would need to to be addressed before considering it production ready:

*   There is no paging or sorting in the contact list
*   There is no search capability
*   Formatting prompts for the zip code and phone number would be helpful
*   Validate the state against a list of state codes
*   Address lookup
*   Alternative formats for international addresses and phone numbers
*   Toast alerts to notify the user of actions performed
*   Improve error messaging and logging in the UI