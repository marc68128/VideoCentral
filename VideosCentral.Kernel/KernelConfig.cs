namespace VideosCentral.Kernel
{
    /// <summary>
    /// Static class used to configure dependancy injection.
    /// </summary>
    static public class KernelConfig
    {
        /// <summary>
        /// <see cref="Kernel.RegisterInstance{T}"/>
        /// </summary>
        static public void RegisterInstance<T>(T instance)
        {
            Kernel.RegisterInstance(instance);
        }
    }
}