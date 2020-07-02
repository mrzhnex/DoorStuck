using Harmony;
using EXILED;

namespace DoorStuck
{
    public class MainSettings : Plugin
    {
        public override string getName => "DoorStuck";
        private HarmonyInstance harmonyInstance;
        public override void OnEnable()
        {
            harmonyInstance = HarmonyInstance.Create("slay.with.door.by.innocence");
            harmonyInstance.PatchAll();
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            if (harmonyInstance != null)
                harmonyInstance.UnpatchAll();
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}