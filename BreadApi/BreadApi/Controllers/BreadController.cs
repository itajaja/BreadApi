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
      var schema = new JsonSchema {Type = JsonSchemaType.Object, Title = "parameters"};
      if (pars.Any())
        schema.Properties = new Dictionary<string, JsonSchema>(pars.Count());
      for (var i = 0; i < pars.Count(); i++)
      {
        var par = generator.Generate(pars[i].ParameterType);
//        generator.ContractResolver.ResolveContract(typeof (string)).DefaultCreator = () =>  { return new object(); };
        par.Title = pars[i].Name;
        schema.Properties.Add(par.Title, par);
      }
      JObject res = JsonConvert.DeserializeObject(schema.ToString()) as JObject;
      //refactor!
      var types = res.Descendants().OfType<JProperty>().Where(t => t.Name == "type");
      foreach (var type in types.Where(t => t.Value is JArray))
      {
        var val = type.Value as JArray;
        if (val.Count == 2 && val.Last is JValue && ((JValue)val.Last).Value.ToString() == "null")
        {
          type.Value = val.First;
        }
      }
      return res;
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
