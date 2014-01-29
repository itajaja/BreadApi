﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Hylasoft.BreadApi.Services;
using Hylasoft.BreadEngine;

namespace Hylasoft.BreadApi.Controllers
{
  public class BreadController : ApiController
  {

    private readonly List<BreadLoader> _breads;

    public BreadController()
    {
      _breads = new List<BreadLoader>();
      foreach (var file in Config.Val(Config.Keys.BreadDlls).Split(','))
      {
        var path = Path.Combine(Config.Val(Config.Keys.PathToSit), file);
        _breads.Add(new BreadLoader(path));
      }
    }


    [HttpPost]
    public IList Select(string bread, string breadClass, SelectQuery sq)
    {
      var breadInstance =
        _breads.Single(b => String.Equals(b.Name, bread, StringComparison.CurrentCultureIgnoreCase))
          .Breads.Single(b => String.Equals(b.Name, breadClass, StringComparison.CurrentCultureIgnoreCase));
      return breadInstance.Select(sq.Condition,sq.StartRowIndex,sq.MaximumRows,sq.SortBy);
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