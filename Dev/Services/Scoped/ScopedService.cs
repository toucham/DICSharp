using DICSharpDev.Lib;

namespace DICSharpDev.Services.Scoped
{
    [DILifetime(ServiceLifetime.Scoped)]
    public class ScopedService : IScopedService
    {
        public readonly long id;
        public ScopedService()
        {
            id = new Random().NextInt64();
        }

        public long Id => id;
    }
}