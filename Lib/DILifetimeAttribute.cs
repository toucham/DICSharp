namespace DICSharp.Lib
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class DILifetimeAttribute : Attribute
    {
        public readonly ServiceLifetime lifetime;
        public DILifetimeAttribute(ServiceLifetime lifetime)
        {
            this.lifetime = lifetime;
        }
    }
}