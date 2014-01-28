using System.Configuration;

namespace Hylasoft.BreadApi.Services
{
  public class Config
  {
    public static string Val(Keys key)
    {
      return ConfigurationManager.AppSettings[key.ToString()];
    }

    public enum Keys
    {
      PluginsFolder,
      PathToSit,
      BreadDlls
    }
  }
}