using DICSharp.Lib;

namespace DICSharp.Services.Singleton
{
    [DILifetime(ServiceLifetime.Singleton)]
    public class SingletonService : ISingletonService
    {
        public readonly long id;
        public SingletonService()
        {
            id = new Random().NextInt64();
        }

        public long Id => id;
    }
}