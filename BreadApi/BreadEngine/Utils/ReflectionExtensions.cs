using System;
using System.Linq;

namespace Hylasoft.BreadEngine.Utils
{
  public static class ReflectionExtensions
  {
    /// <summary>
    /// Get the generic type of the closest generic parent
    /// </summary>
    /// <returns>The generic type of the given type</returns>
    public static Type GetGenericParent(this Type t)
    {
      for (var p = t; p!=null ; p = p.BaseType)
      {
        if (p.IsGenericType)
          return p.GenericTypeArguments.First();
      }
      return null;
    }
  }
}