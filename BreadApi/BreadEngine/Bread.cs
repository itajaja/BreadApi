using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hylasoft.BreadEngine.Utils;

namespace Hylasoft.BreadEngine
{
  /// <summary>
  /// Defines a Bread and its methods
  /// </summary>
  public partial class Bread
  {
    /// <summary>
    /// Gets the underlying bread type
    /// </summary>
    public Type BreadType { get; private set; }

    /// <summary>
    /// Gets the underlying entity Type
    /// </summary>
    public Type EntityType { get; private set; }

    /// <summary>
    /// Gets the name of the Bread
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets a list of all the available Bread Methods
    /// </summary>
    public IList<string> Methods { get; private set; }

    /// <summary>
    /// Initializes a new Bread
    /// </summary>
    /// <param name="breadType"></param>
    public Bread(Type breadType)
    {
      if (!IsCorrectBreadType(breadType))
        throw new ArgumentException("the breadType parameter is not a valid Bread Type.");
      BreadType = breadType;
      EntityType = breadType.GetGenericParent();
      Name = breadType.Name.Split('_')[0];
      Methods = breadType.GetMethods().Where(m => BreadMethods.Contains(m.Name)).Select(m => m.Name).ToList();
      _defaultConstructor = breadType.GetConstructor(new Type[] { });
      if (_defaultConstructor == null)
        throw new ArgumentException("the breadType doesn't contain a default constructor!");
      ResetInstance();
    }

    /// <summary>
    /// Invoke the specified method on the bread
    /// </summary>
    /// <param name="methodName">The method name. The method must be a valid bread type, i.e. it must appear in the "Methods" property</param>
    /// <param name="parameters">The parameters that are used in the method</param>
    /// <returns>The result of the invocation</returns>
    public object InvokeMethod(string methodName, object[] parameters)
    {
      var method = GetMethod(methodName);
      return method.Invoke(_bread, parameters);
    }

    /// <summary>
    /// Gets the parameters for the specified method
    /// </summary>
    /// <param name="methodName">The method name. The method must be a valid bread type, i.e. it must appear in the "Methods" property</param>
    /// <returns>The list of parameters</returns>
    public ParameterInfo[] GetMethodParams(string methodName)
    {
      var method = GetMethod(methodName);
      return method.GetParameters();
    }

    /// <summary>
    /// Resets the current bread instance, if the need occurs
    /// </summary>
    public void ResetInstance()
    {
      _bread = _defaultConstructor.Invoke(new object[] { });
    }
    
    #region private

    //the bread instance
    private object _bread;
    //the default constructor of the bread
    private readonly ConstructorInfo _defaultConstructor;

    private MethodInfo GetMethod(string methodName)
    {
      var method = BreadType.GetMethods().SingleOrDefault(m => m.Name == methodName);
      if (!Methods.Contains(methodName) || method == null)
        throw new ArgumentException("The class " + Name + " doesn't contain the method " + methodName +
                                    " or it's not a valid bread method");
      return method;
    }

    #endregion
  }
}