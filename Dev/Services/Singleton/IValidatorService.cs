namespace DICSharpDev.Services.Singleton
{
    public interface IValidatorService<T>
    {
        public void ValidateType(T input);
    }
}