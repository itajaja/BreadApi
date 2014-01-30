using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Hylasoft.BreadApi.Services;
using Hylasoft.BreadEngine;
using Newtonsoft.Json.Linq;

namespace Hylasoft.BreadApi.Controllers
{
  public class BreadController : ApiController
  {

    private readonly List<BreadLoader> _breads = BreadService.Get();

    [HttpPost]
    public object Invoke(string bread, string breadClass, string method, JArray query)
    {
      var loader = _breads.SingleOrDefault(b => String.Equals(b.Name, bread, StringComparison.CurrentCultureIgnoreCase));
      if (loader == null)
        throw new ArgumentException("Couldn't find the Bread" + bread);
      var breadInstance = loader.Breads.Single(b => String.Equals(b.Name, breadClass, StringComparison.CurrentCultureIgnoreCase));
      if (breadInstance == null)
        throw new ArgumentException("Couldn't find the class" + breadClass + " inside " + bread);
      var pars = breadInstance.GetMethodTypes(method);
      var objectQuery = new object[pars.Count()];
      for (var i = 0; i < pars.Count(); i++)
      {
        objectQuery[i] = query[i].ToObject(pars[i].ParameterType);
      }
      return breadInstance.InvokeMethod(method, objectQuery);
    }
  }

  public class SelectQuery
  {


    public string Condition { get; set; }
    public int StartRowIndex { get; set; }
    public int MaximumRows { get; set; }
    public string SortBy { get; set; }
  }
}
