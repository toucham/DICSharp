# DICSharp

## Objective

To be able to automatically register services without having to explicitly call the `.Add{LIFETIME}()` method. The features will include the following:

- using attributes to register services as Singleton, Scope, or Transient.
- only using the extension `AddServicesToContainer()` in `Program.cs` to register all the services that have the attribute
- support both multiproject folder structure and simplified folder structure

### Possible Cases to Cover

These are all the possible features and cases that the library need to cover. This means that the library will not get unexpected behavior in any of these cases.

- [x] able to add service as singleton lifetime
- [x] able to add service as transient lifetime
- [x] able to add service as scoped lifetime
- [ ] able to resolve multiple implementations for one service
- [ ] able to resolve service with generic
- [ ] can still add services in multi-assemblies project

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
