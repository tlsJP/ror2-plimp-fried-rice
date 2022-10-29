using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace com.thejpaproject
{
    [BepInDependency(R2API.R2API.PluginGUID)]

    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(LanguageAPI))]

    public class PlimpFriedRice : BaseUnityPlugin
    {

        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "com.thejpaproject.com.plimpfriedrice";
        public const string PluginName = "Plimp Fried Rice";
        public const string PluginVersion = "1.0.0";



        public void Awake()
        {

            Log.Init(Logger);

        }



    }
}
