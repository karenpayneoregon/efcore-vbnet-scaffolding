# Interceptors

Entity Framework Core (EF Core) interceptors enable interception, `modification`, and/or `suppression` of EF Core operations. This includes low-level database operations such as executing a command, as well as higher-level operations, such as calls to SaveChanges.

Interceptors are different from logging and diagnostics in that they allow modification or suppression of the operation being intercepted. Simple logging or [Microsoft.Extensions.Logging](https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/extensions-logging?tabs=v3) are better choices for logging.

[Full documentation](https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/interceptors)