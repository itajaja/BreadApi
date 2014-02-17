using System.Collections.Generic;
using System.IO;
using Hylasoft.BreadEngine;

namespace Hylasoft.BreadApi.Services
{
  public static class BreadService
  {
    private static List<BreadLoader> _breads;

    private static void InitializeBreads()
    {
      _breads = new List<BreadLoader>();
      foreach (var file in Config.Val(Config.Keys.BreadDlls).Split(','))
      {
        var path = Path.Combine(Config.Val(Config.Keys.PathToSit), file);
        _breads.Add(new BreadLoader(path));
      }
    }

    /// <summary>
    /// Gets the list of available breads. Loads the dll only the first time it gets called
    /// </summary>
    /// <returns>The list of available breads</returns>
    public static List<BreadLoader> Get()
    {
      if (_breads == null)
        InitializeBreads();  
      return _breads;
    }

    /// <summary>
    /// Resets the BreadService and force loading the dll again
    /// </summary>
    /// <returns>The list of available breads</returns>
    public static void ResetBreadService()
    {
        InitializeBreads();
    }
  }
}