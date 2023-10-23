# DICSharp

To be able to automatically register services without having to explicitly call the `.Add{LIFETIME}()` method. The features will include the following:

- using attributes to register services as Singleton, Scope, or Transient.
- only using the extension `AddServicesToContainer()` in `Program.cs` to register all the services that have the attribute
- support both multiproject folder structure and simplified folder structure

## Project

### Structure

- The root project will contain the .NET Standard 2.0 lib for running the user to pull and run the automatatic service registeration extension.
- `Dev` : contains the ASP.NET project that contains the mock extension to automaticreated inside

### Possible Cases to Cover

These are all the possible features and cases that the library need to cover. This means that the library will not get unexpected behavior in any of these cases.

- [x] able to add service as singleton lifetime
- [x] able to add service as transient lifetime
- [x] able to add service as scoped lifetime
- [ ] able to resolve multiple implementations for one service
- [x] able to resolve service with generic
- [ ] can still add services in multi-assemblies project## D### Possible Cases to Cover

## Dependency Injection

### Multiple Implementations

If there are multiple implementations of a service registered, only the latest one is injected:

```c#
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IMessageWriter, ConsoleMessageWriter>();
builder.Services.AddSingleton<IMessageWriter, LoggingMessageWriter>();
builder.Services.AddSingleton<ExampleService>();
```

In this case, `LoggingMessagewriter` is injected when a class has `IMessageWriter` in its constructor. A class can get all the implementations that are registered by wrapping the interface in `IEnumerable<IMessageWriter>`.

### Implementation with Generic Service

```c#
public class StringGenericService : IGenericService<string> {
    ...
}

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IGenericService<T>, GenericService<T>>();
```

### Services with Different Lifetime

It is possible to register the same services with different lifetime; however, the container will inject the lifetime from shortest to longest. For example:

```c#
services.AddTransient<IMyService, MyService>();
services.AddSingleton<IMyService, MyService>();
```

The service container will resolve with a transient `MyService` object, no matter where it is requested. Therefore, it is not **RECOMMENDED** to add different lifetime for one service.
