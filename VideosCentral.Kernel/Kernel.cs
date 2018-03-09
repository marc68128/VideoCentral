using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace VideosCentral.Kernel
{
    /// <summary>
    /// Static class used to managed dependancy injection.
    /// </summary>
    static public class Kernel
    {
        #region Instance variables

        /// <summary>
        /// Unity container used to register and resolve types. 
        /// </summary>
        static private readonly UnityContainer s_Container;

        #endregion //Instance variables

        #region Constructors

        /// <summary>
        /// Kernel constructor.
        /// Create UnityContainer instance.
        /// </summary>
        static Kernel()
        {
            s_Container = new UnityContainer();
        }

        #endregion //Constructors

        #region Methods

        /// <summary>
        /// <see cref="UnityContainer.Resolve"/>
        /// </summary>
        static public T Resolve<T>()
        {
            return s_Container.Resolve<T>();
        }

        /// <summary>
        /// <see cref="UnityContainer.Resolve"/>
        /// </summary>
        static public bool TryResolve<T>(out T output)
        {
            try
            {
                output = s_Container.Resolve<T>();
                return true;
            }
            catch (Exception)
            {
                output = default(T);
                return false;
            }
        }

        /// <summary>
        /// <see cref="UnityContainer.Resolve"/>
        /// </summary>
        static public object Resolve(Type type)
        {
            return s_Container.Resolve(type);
        }

        /// <summary>
        /// <see cref="UnityContainer.Resolve"/>
        /// </summary>
        static public bool TryResolve(Type type, out object output)
        {
            try
            {
                output = s_Container.Resolve(type);
                return true;
            }
            catch (Exception)
            {
                output = null;
                return false;
            }
        }

        /// <summary>
        /// <see cref="UnityContainer.ResolveAll"/>
        /// </summary>
        static public IEnumerable<T> ResolveCollection<T>()
        {
            return s_Container.ResolveAll(typeof(T)).Select(i => (T)i);
        }

        /// <summary>
        /// <see cref="UnityContainer.ResolveAll"/>
        /// </summary>
        static public IEnumerable<object> ResolveCollection(Type type)
        {
            return s_Container.ResolveAll(type);
        }

        /// <summary>
        /// <see cref="UnityContainer.RegisterInstance"/>
        /// </summary>
        static internal void RegisterInstance<T>(T instance)
        {
            s_Container.RegisterInstance<T>(instance);
        }

        #endregion //Methods
    }
}