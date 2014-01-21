using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.MES.Breads;

namespace Hylasoft.BreadEngine
{
    /// <summary>
    /// Defines a Bread and its methods
    /// </summary>
    public class Bread
    {
        /// <summary>
        /// checks if whether or not the given type is a correct Bread Type
        /// </summary>
        /// <param name="type">the Type to check</param>
        /// <returns></returns>
        public static bool IsCorrectBreadType(Type type)
        {
            return typeof (EntityWithPlugins_BREAD).IsAssignableFrom(type) && !type.IsGenericType;
        }

        /// <summary>
        /// Defines the list of available bread Methods
        /// </summary>
        public static readonly IReadOnlyList<string> BreadMethods = new List<string>{"Create",
            "Delete",
            "DeleteEx",
            "DeleteExXml",
            "Edit",
            "EditEx",
            "EditExXml",
            "Select",
            "SelectCount",
            "SelectByPK",
            "SelectEx",
            "SelectExCount",
            "SelectExDistinct",
            "SelectExRank",
            "SelectExXml",
            "SelectExXmlCount",
            "SelectExXmlDistinct",
            "SelectExXmlRank",
            "SelectRank",
            "SetCurrentTransactionHandle",
            "SetCurrentUser",
            "UnsetCurrentTransactionHandle"};

        /// <summary>
        /// Gets the underlying bread type
        /// </summary>
        public Type BreadType { get; private set; }

        /// <summary>
        /// Gets the name of the Bread
        /// </summary>
        public string Name { get; private set; }

        public IList<string> Methods { get; private set; } 

        /// <summary>
        /// initializes a new Bread
        /// </summary>
        /// <param name="breadType"></param>
        public Bread(Type breadType)
        {
            if (!IsCorrectBreadType(breadType))
                throw new ArgumentException("the breadType parameter is not a valid Bread Type.");
            BreadType = breadType;
            Name = breadType.Name.Split('_')[0];
            Methods = breadType.GetMethods().Where(m => BreadMethods.Contains(m.Name)).Select(m => m.Name).ToList();
        }
    }
}