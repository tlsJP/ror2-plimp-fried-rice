
using BepInEx;
using BepInEx.Logging;
using R2API;
using R2API.Networking;
using R2API.Utils;

using System.IO;
using System.Reflection;


namespace com.thejpaproject
{
    [BepInDependency(R2API.R2API.PluginGUID)]

    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    [R2APISubmoduleDependency(nameof(SoundAPI), nameof(ItemAPI), nameof(NetworkingAPI))]

    public class PlimpFriedRice : BaseUnityPlugin
    {
        private static protected ManualLogSource _logger;
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "com.thejpaproject";
        public const string PluginName = "PlimpFriedRice";
        public const string PluginVersion = "1.0.0";



        public void Awake()
        {
            _logger = BepInEx.Logging.Logger.CreateLogSource("PlimpFriedRice");


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


            On.RoR2.HealthComponent.TakeDamage += OnTakeDamage;



        }



        private void OnTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, RoR2.HealthComponent self, RoR2.DamageInfo damageInfo)
        {
            orig(self, damageInfo);
            _logger.LogInfo("play?");
            // AkSoundEngine.PostEvent(132404053, PlayerCharacterMasterController.instances[0].master.resolvedBodyInstance);



            RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);

            // AkSoundEngine.PostEvent(661497120, self.gameObject);



            // RoR2.Util.PlaySound("Play_item_void_critGlasses", self.gameObject);
            // RoR2.Util.PlaySound("Play_item_void_critGlasses", self.gameObject);
            // RoR2.Util.PlaySound("Play_item_void_critGlasses", self.gameObject);
            // RoR2.Util.PlaySound("Play_item_void_critGlasses", self.gameObject);


        }



    }
}
