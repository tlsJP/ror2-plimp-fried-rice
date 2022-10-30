
using BepInEx;
using BepInEx.Logging;
using R2API;
using R2API.Networking;
using R2API.Utils;

using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;

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



            var missileVoidProjectilePrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MissileVoid/MissileVoidProjectile.prefab").WaitForCompletion();
            var controller = missileVoidProjectilePrefab.GetComponent<RoR2.Projectile.MissileController>;
            var missileVoid = RoR2.ItemCatalog.FindItemIndex(RoR2.DLC1Content.Items.MissileVoid.name);

            On.RoR2.Projectile.MissileController.Awake += FuckMeUp;
            On.RoR2.Projectile.ProjectileController.Awake += FuckMeUp2;
            On.RoR2.Projectile.ProjectileController.OnTriggerEnter += OnTrigger;
            On.RoR2.Projectile.ProjectileController.OnEnable += OnEnable;
            On.RoR2.Projectile.ProjectileController.Start += Start;

            // On.RoR2.HealthComponent.TakeDamage += OnTakeDamage;


        }

        private void Start(On.RoR2.Projectile.ProjectileController.orig_Start orig, RoR2.Projectile.ProjectileController self)
        {
            orig(self);
             _logger.LogInfo($" {self.name} - Start");
            RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);
        }

        private void OnEnable(On.RoR2.Projectile.ProjectileController.orig_OnEnable orig, RoR2.Projectile.ProjectileController self)
        {
            orig(self);

            _logger.LogInfo($" {self.name} - OnEnable");
            RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);
        }

        private void FuckMeUp(On.RoR2.Projectile.MissileController.orig_Awake orig, RoR2.Projectile.MissileController self)
        {
            orig(self);

            _logger.LogInfo($" {self.name} - fuck me up!");
            RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);
        }

        private void FuckMeUp2(On.RoR2.Projectile.ProjectileController.orig_Awake orig, global::RoR2.Projectile.ProjectileController self)
        {
            orig(self);

            _logger.LogInfo($" {self.name} - fuck me up2!");
            RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);
        }

        private void OnTrigger(On.RoR2.Projectile.ProjectileController.orig_OnTriggerEnter orig, global::RoR2.Projectile.ProjectileController self, Collider collider)
        {
            orig(self, collider);

            _logger.LogInfo($" {self.name} - onTrigger !");
            RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);
        }

        private void OnTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, RoR2.HealthComponent self, RoR2.DamageInfo damageInfo)
        {
            orig(self, damageInfo);

            RoR2.Util.PlaySound("deepfriedplimp", self.gameObject);


        }


    }
}
