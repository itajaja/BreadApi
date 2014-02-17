using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
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
    public object Invoke(string bread, string breadClass, string method, JObject query)
    {
      if (string.IsNullOrEmpty(breadClass))
        return BreadList(bread);
      var breadInstance = LoadBread(bread, breadClass);
      if (string.IsNullOrEmpty(method))
        return breadInstance.Methods;
      var pars = breadInstance.GetMethodParams(method);
      var objectQuery = new object[pars.Count()];
      for (var i = 0; i < pars.Count(); i++)
        if (query[pars[i].Name] != null)
          objectQuery[i] = query[pars[i].Name].ToObject(pars[i].ParameterType);
        else
          objectQuery[i] = pars[i].ParameterType.IsValueType ? Activator.CreateInstance(pars[i].ParameterType) : null;
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
        schema.Properties.Add(pars[i].Name, par);
      }
      ModifyJsonSchema(schema);
      var res = JsonConvert.DeserializeObject(schema.ToString()) as JObject;
      return res;
    }

    /// <summary>
    /// Prepare the schema to be JsonForm Friendly, adding titles, default values and correct types
    /// </summary>
    /// <param name="schema">the schema to modify</param>
    private static void ModifyJsonSchema(JsonSchema schema)
    {
      if (schema.Properties == null)
        return;
      foreach (var property in schema.Properties)
      {
        var val = property.Value;
        var key = property.Key;
        //sets the title of the property as the key value
        val.Title = key;
        if (val.Type.HasValue && val.Type.Value.HasFlag(JsonSchemaType.String))
        {
          //sets the default value of the string to ""
          val.Default = "";
          //the string shouldn't be required
          val.Required = false;
        }
        if (val.Type.HasValue && val.Type.Value.HasFlag(JsonSchemaType.Null))
          //remove the null type
          val.Type &= ~JsonSchemaType.Null;
        ModifyJsonSchema(val);
      }
    }

    private Bread LoadBread(string bread, string breadClass)
    {
      var loader = FindLoader(bread);
      var breadInstance =
        loader.Breads.SingleOrDefault(b => String.Equals(b.Name, breadClass, StringComparison.CurrentCultureIgnoreCase));
      if (breadInstance == null)
        throw new ArgumentException("Couldn't find the class" + breadClass + " inside " + bread);
      return breadInstance;
    }

    private BreadLoader FindLoader(string bread)
    {
      var loader = _breads.SingleOrDefault(b => String.Equals(b.Name, bread, StringComparison.CurrentCultureIgnoreCase));
      if (loader == null)
        throw new ArgumentException("Couldn't find the Bread " + bread);
      return loader;
    }

    private object BreadList(string bread)
    {
      return FindLoader(bread).Breads;
    }
  }
}
