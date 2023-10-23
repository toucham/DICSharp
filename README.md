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

These are all the possible features and cases that the auto-register services lib able to do. This means that the library will not get unexpected behavior in any of these cases.

- [x] add service as singleton lifetime
- [x] add service as transient lifetime
- [x] add service as scoped lifetime
- [x] resolve multiple implementations for one service
- [x] resolve service with generic
- [x] add services in multi-assemblies project

For multi-assemblies project, we are assuming that project reference assemblies will not have `PublicKeyToken`. It is currently adding only the assemblies without `PublicKeyToken`.

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
