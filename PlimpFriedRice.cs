
using BepInEx;
using BepInEx.Logging;
using BepInEx.Bootstrap;
using R2API;
using R2API.Networking;
using R2API.Utils;
using RiskOfOptions;
using System.IO;
using System.Reflection;

namespace com.thejpaproject
{
  [BepInDependency(R2API.R2API.PluginGUID)]
  [BepInDependency("com.rune580.riskofoptions")]
  [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

  [R2APISubmoduleDependency(nameof(SoundAPI), nameof(ItemAPI), nameof(NetworkingAPI))]

  public class PlimpFriedRice : BaseUnityPlugin
  {
    private static protected ManualLogSource _logger;
    public const string PluginGUID = PluginAuthor + "." + PluginName;
    public const string PluginAuthor = "com.thejpaproject";
    public const string PluginName = "PlimpFriedRice";
    public const string PluginVersion = "1.0.2";

    private static readonly bool s_rooEnabled = Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions");

    public void Awake()
    {
      _logger = BepInEx.Logging.Logger.CreateLogSource("PlimpFriedRice");

      if (s_rooEnabled)
      {
        ConfigureRiskOfOptions();
      }


      _logger.LogInfo("trying...");


      using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("com.thejpaproject.plimpfriedrice.bnk"))
      {
        var bytes = new byte[resourceStream.Length];
        var readBytes = resourceStream.Read(bytes, 0, bytes.Length);
        _logger.LogInfo($"Loaded {readBytes} bytes");
        // var soundId =SoundBanks.Add(bytes);
        var soundId = SoundAPI.SoundBanks.Add(bytes);
        _logger.LogInfo($"Bank added as ID : {soundId}");
      }

      On.RoR2.Orbs.OrbEffect.Start += OrbStart;

      _logger.LogInfo("Completed");
    }

    private static void OrbStart(On.RoR2.Orbs.OrbEffect.orig_Start orig, RoR2.Orbs.OrbEffect self)
    {
      orig(self);
      _logger.LogInfo($" {self.name} - OrbStart");
      if (self.name.StartsWith("MissileVoidOrbEffect"))
      {
        RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);
      }

    }

    void ConfigureRiskOfOptions()
    {
      ModSettingsManager.SetModDescription("Enable/disable plimp fried rice");
    }

  }
}
