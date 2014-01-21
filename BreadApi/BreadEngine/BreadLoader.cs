using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Siemens.SimaticIT.MES.Breads;

namespace Hylasoft.BreadEngine
{
    class BreadLoader
    {
        /// <summary>
        /// Gets the current assembly
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Gets or sets the displayable name of the current Bread
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets All the Bread Types
        /// </summary>
        public IList<Type> Breads { get; private set; }

        /// <summary>
        /// Initializes a new BreadLoader object using the given assembly
        /// </summary>
        /// <param name="assembly">The assembly to load</param>
        public BreadLoader(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            Assembly = assembly;
            Breads = assembly.GetTypes().Where(t => typeof(EntityWithPlugins_BREAD).IsAssignableFrom(t) && !t.IsGenericType).ToList();
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
