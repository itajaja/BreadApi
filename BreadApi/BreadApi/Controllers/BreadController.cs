using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Hylasoft.BreadApi.Services;
using Hylasoft.BreadEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Hylasoft.BreadApi.Controllers
{
  public class BreadController : ApiController
  {

    private readonly List<BreadLoader> _breads = BreadService.Get();

    [HttpPost]
    public object Invoke(string bread, string breadClass, string method, JArray query)
    {
      var breadInstance = LoadBread(bread, breadClass);
      var pars = breadInstance.GetMethodParams(method);
      var objectQuery = new object[pars.Count()];
      for (var i = 0; i < pars.Count(); i++)
        objectQuery[i] = query[i].ToObject(pars[i].ParameterType);
      var result = breadInstance.InvokeMethod(method, objectQuery);
      return result;
    }

    [HttpPost]
    public object Params(string bread, string breadClass, string method)
    {
      var breadInstance = LoadBread(bread, breadClass);
      var pars = breadInstance.GetMethodParams(method);
      var objectQuery = new object[pars.Count()];
      var generator = new JsonSchemaGenerator();
      for (var i = 0; i < pars.Count(); i++)
      {
        var schema = generator.Generate(pars[i].ParameterType);
        objectQuery[i] = JsonConvert.DeserializeObject(schema.ToString());
      }
      return objectQuery;
    }

    private Bread LoadBread(string bread, string breadClass)
    {
      var loader = _breads.SingleOrDefault(b => String.Equals(b.Name, bread, StringComparison.CurrentCultureIgnoreCase));
      if (loader == null)
        throw new ArgumentException("Couldn't find the Bread" + bread);
      var breadInstance =
        loader.Breads.Single(b => String.Equals(b.Name, breadClass, StringComparison.CurrentCultureIgnoreCase));
      if (breadInstance == null)
        throw new ArgumentException("Couldn't find the class" + breadClass + " inside " + bread);
      return breadInstance;
    }
  }
}
