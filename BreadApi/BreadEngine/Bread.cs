using System;
using System.Collections;
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
    /// Invoke the Select method on the bread
    /// </summary>
    /// <param name="condition">The condition parameter is a string containing the filter condition for the instances. The syntax is the same as the WHERE clause of an SQL Server query: instead of field names, BREAD property names are specified enclosed in curly bracket</param>
    /// <param name="startRowIndex">Maximum number of instances returned. To invoke the method without paging the results, set to -1</param>
    /// <param name="maximumRows">Zero-based index of the first instance returned. To invoke the method without paging the results, set to -1</param>
    /// <param name="sortby">The sortby parameter is a string containing the list of entity properties to sort the results by. The list is comma-separated and the property names must be enclosed in curly brackets, unless a single property is specified. ASC and DESC (case insensitive) can be used to reverse ordering, as in a common SQL order by clause</param>
    /// <returns></returns>
    public IList Select(string condition = "", int startRowIndex = -1, int maximumRows = -1, string sortby = "")
    {
      var select = BreadType.GetMethods().Single(m => m.Name == BreadMethodSelect);
      return (IList)select.Invoke(_bread, new object[] { sortby, startRowIndex, maximumRows, condition });
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
    
    #endregion
  }
}