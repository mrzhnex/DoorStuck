using Exiled.API.Features;
using HarmonyLib;

namespace DoorStuck
{
    public class MainSettings : Plugin<Config>
    {
        public override string Name => nameof(DoorStuck);
        public Harmony Harmony { get; set; }
        public override void OnEnabled()
        {
            Harmony = new Harmony("slay.with.door.by.innocence");
            Harmony.PatchAll();
            Log.Info(Name + " on");
        }

        public override void OnDisabled()
        {
            if (Harmony != null)
                Harmony.UnpatchAll();
            Log.Info(Name + " off");
        }
    }
}