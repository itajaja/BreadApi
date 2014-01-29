using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hylasoft.BreadApi.Services;
using Hylasoft.BreadEngine;
using m = Hylasoft.BreadApi.Models;

namespace Hylasoft.BreadApi.Controllers
{
  public class HomeController : Controller
  {
    private readonly List<BreadLoader> _breads = BreadService.Get();

    public ActionResult Index()
    {
      var model = new m.IndexModel{Breads = _breads.Select(b => b.Name).ToList()};
      return View(model);
    }

    public ActionResult Bread(string id)
    {
      var breadInstance = _breads.Single(b => String.Equals(b.Name, id, StringComparison.CurrentCultureIgnoreCase));
      var model = new m.BreadModel { Bread = new m.BreadLoader(breadInstance)};
      return View(model);
    }
  }
}
