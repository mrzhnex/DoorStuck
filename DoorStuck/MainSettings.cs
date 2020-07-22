using Harmony;
using EXILED;

namespace DoorStuck
{
    public class MainSettings : Plugin
    {
        public override string getName => nameof(DoorStuck);
        public HarmonyInstance HarmonyInstance { get; set; }
        public override void OnEnable()
        {
            HarmonyInstance = HarmonyInstance.Create("slay.with.door.by.innocence");
            HarmonyInstance.PatchAll();
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            if (HarmonyInstance != null)
                HarmonyInstance.UnpatchAll();
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}