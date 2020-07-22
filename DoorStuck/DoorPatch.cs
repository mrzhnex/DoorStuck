using Harmony;

namespace DoorStuck
{
    [HarmonyPatch(typeof(Door), "ChangeState")]
    public class DoorPatch
    {
        [HarmonyPrefix]
        public static void Prefix (Door __instance, bool force)
        {
            if (__instance.curCooldown >= 0f || __instance.moving.moving || (__instance.locked && !force) || __instance.gameObject == null)
                return;
            if (__instance.NetworkisOpen)
                __instance.gameObject.AddComponent<DoorAndPlayerTimeCheck>();
        }
    }
}