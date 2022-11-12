
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using R2API;
using R2API.Networking;
using R2API.Utils;
using RiskOfOptions;
using RiskOfOptions.Options;
using System.IO;
using System.Reflection;
using UnityEngine;

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
    public const string PluginVersion = "1.1.1";

    // private static readonly bool s_rooEnabled = Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions");

    private static ConfigEntry<bool> s_enabled;

    public void Awake()
    {
      _logger = BepInEx.Logging.Logger.CreateLogSource("PlimpFriedRice");


      ConfigureRiskOfOptions();



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

      _logger.LogInfo("Plimp has been deep fried");
    }

    private static void OrbStart(On.RoR2.Orbs.OrbEffect.orig_Start orig, RoR2.Orbs.OrbEffect self)
    {
      orig(self);
      var enabled = ((ConfigEntry<bool>)s_enabled).Value;

      if (enabled && self.name.StartsWith("MissileVoidOrbEffect"))
      {
        RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);
      }

    }

    void ConfigureRiskOfOptions()
    {

      LoadSprite();
      ModSettingsManager.SetModDescription("Enable/disable plimp fried rice");

      s_enabled = this.Config.Bind("General", "Plimp Fried Rice", false, "Do you want plimp fried rice? Do you like your plimps deep fried?");
      ModSettingsManager.AddOption(new CheckBoxOption(s_enabled));
      HandleEvent(s_enabled, null);
      s_enabled.SettingChanged += HandleEvent;
    }

    void HandleEvent(object x, System.EventArgs _)
    {
      var enabled = ((ConfigEntry<bool>)x).Value;
    }

    private void LoadSprite()
    {
      using var stream = GetType().Assembly.GetManifestResourceStream("plimp");
      var texture = new Texture2D(0, 0);
      var imgdata = new byte[stream.Length];
      stream.Read(imgdata, 0, imgdata.Length);
      if (ImageConversion.LoadImage(texture, imgdata))
      {
        var sprite = Sprite.Create(
                   texture,
                   new Rect(0, 0, texture.width, texture.height),
                   new Vector2(0, 0)
                 );
        ModSettingsManager.SetModIcon(sprite);
      }
    }

  }

}
