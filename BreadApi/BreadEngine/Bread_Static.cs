using System;
using System.Collections.Generic;
using Siemens.SimaticIT.MES.Breads;

namespace Hylasoft.BreadEngine
{
  public partial class Bread
  {
    public const string BreadMethodCreate = "Create";
    public const string BreadMethodDelete = "Delete";
//    public const string BreadMethodDeleteEx = "DeleteEx";
//    public const string BreadMethodDeleteExXml = "DeleteExXml";
    public const string BreadMethodEdit = "Edit";
//    public const string BreadMethodEditEx = "EditEx";
//    public const string BreadMethodEditExXml = "EditExXml";
    public const string BreadMethodSelect = "Select";
    public const string BreadMethodSelectCount = "SelectCount";
    public const string BreadMethodSelectByPk = "SelectByPK";
//    public const string BreadMethodSelectEx = "SelectEx";
//    public const string BreadMethodSelectExCount = "SelectExCount";
//    public const string BreadMethodSelectExDistinct = "SelectExDistinct";
//    public const string BreadMethodSelectExRank = "SelectExRank";
//    public const string BreadMethodSelectExXml = "SelectExXml";
//    public const string BreadMethodSelectExXmlCount = "SelectExXmlCount";
//    public const string BreadMethodSelectExXmlDistinct = "SelectExXmlDistinct";
//    public const string BreadMethodSelectExXmlRank = "SelectExXmlRank";
    public const string BreadMethodSelectRank = "SelectRank";
    public const string BreadMethodSetCurrentTransactionHandle = "SetCurrentTransactionHandle";
    public const string BreadMethodSetCurrentUser = "SetCurrentUser";
    public const string BreadMethodUnsetCurrentTransactionHandle = "UnsetCurrentTransactionHandle";

    /// <summary>
    /// checks if whether or not the given type is a correct Bread Type
    /// </summary>
    /// <param name="type">the Type to check</param>
    /// <returns></returns>
    public static bool IsCorrectBreadType(Type type)
    {
      return typeof(EntityWithPlugins_BREAD).IsAssignableFrom(type) && !type.IsGenericType && !type.IsAbstract;
    }

    /// <summary>
    /// Defines the list of available Bread Methods
    /// </summary>
    public static readonly IReadOnlyList<string> BreadMethods = new List<string>
    {
      BreadMethodCreate,
      BreadMethodDelete,
//      BreadMethodDeleteEx,
//      BreadMethodDeleteExXml,
      BreadMethodEdit,
//      BreadMethodEditEx,
//      BreadMethodEditExXml,
      BreadMethodSelect,
      BreadMethodSelectCount,
      BreadMethodSelectByPk,
//      BreadMethodSelectEx,
//      BreadMethodSelectExCount,
//      BreadMethodSelectExDistinct,
//      BreadMethodSelectExRank,
//      BreadMethodSelectExXml,
//      BreadMethodSelectExXmlCount,
//      BreadMethodSelectExXmlDistinct,
//      BreadMethodSelectExXmlRank,
      BreadMethodSelectRank,
      BreadMethodSetCurrentTransactionHandle,
      BreadMethodSetCurrentUser,
      BreadMethodUnsetCurrentTransactionHandle
    };
  }
}
