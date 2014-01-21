using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hylasoft.BreadEngine
{
    /// <summary>
    /// Exposes the breads inside a bread DLL
    /// </summary>
    class BreadLoader
    {
        /// <summary>
        /// Gets the current assembly
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Gets or sets the displayable name of the current Bread plugin
        /// </summary>
        public string Name
        {
            get { return Assembly.GetName().Name; }
        }

        /// <summary>
        /// Gets all the Bread Types
        /// </summary>
        public IList<Bread> Breads { get; private set; }

        /// <summary>
        /// Initializes a new BreadLoader object using the given assembly
        /// </summary>
        /// <param name="assembly">The assembly to load</param>
        public BreadLoader(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            Assembly = assembly;
            Breads = assembly.GetTypes().Where(Bread.IsCorrectBreadType).Select(t => new Bread(t)).ToList();
        }

        /// <summary>
        /// Initializes a new BreadLoader object using the given path for the assembly
        /// </summary>
        /// <param name="assemblyPath">The absolute path to the assembly to load</param>
        public BreadLoader(string assemblyPath)
            : this(Assembly.LoadFile(assemblyPath))
        {
        }
    }
}
