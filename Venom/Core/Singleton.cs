using System;

namespace Core
{
    /// <summary>
    /// Singleton abstract class for creating easy singleton everywhere.
    /// </summary>
    public abstract class Singleton<T> where T : class
    {
        /// <summary>
        /// Static instance. Needs to use lambda expression
        /// to construct an instance (since constructor is private).
        /// </summary>
        private static readonly Lazy<T> L = new Lazy<T>( () => CreateInstance() );

        /// <summary>
        /// Gets the instance of this singleton.
        /// </summary>
        public static T GetInstance => L.Value;

        /// <summary>
        /// Creates an instance of T via reflection since T's constructor is expected to be private.
        /// </summary>
        private static T CreateInstance()
        {
            return Activator.CreateInstance( typeof( T ), true ) as T;
        }
    }
}
