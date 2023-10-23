using DICSharpDev.Lib;

namespace DICSharpDev.Services.Transient
{
    [DILifetime(ServiceLifetime.Transient)]
    public class TransientService : ITransientService
    {

        public readonly long id;

        public TransientService()
        {
            id = new Random().NextInt64();
        }

        public long Id => id;
    }
}