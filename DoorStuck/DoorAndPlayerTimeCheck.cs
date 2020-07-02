using EXILED.Extensions;
using UnityEngine;

namespace DoorStuck
{
    public class DoorAndPlayerTimeCheck : MonoBehaviour
    {
        private readonly float TimeIsUp = 1.6f;
        private float Timer = 0.0f;
        private bool IsGate = false;
        private Door Door;
        public void Start()
        {
            Door = gameObject.GetComponent<Door>();
            if (Door.DoorName.ToLower().Contains("gate") || Door.DoorName.ToLower().Contains("079") || Door.DoorName.ToLower().Contains("914") || Door.DoorName.ToLower().Contains("372"))
            {
                IsGate = true;
            }
            if (!Door.DoorName.Contains("173"))
            {
                foreach (Door door in Map.Doors)
                {
                    if (door.DoorName == "173" && Vector3.Distance(Door.gameObject.transform.position, door.gameObject.transform.position) < 20.0f)
                    {
                        IsGate = true;
                        break;
                    }
                }
            }
            if (!IsGate)
                Destroy(gameObject.GetComponent<DoorAndPlayerTimeCheck>());
        }

        public void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > TimeIsUp)
            {
                foreach (ReferenceHub p in Player.GetHubs())
                {
                    if (p.GetRole() == RoleType.Spectator || p.GetTeam() == Team.TUT || p.GetRole() == RoleType.Scp079 || p.GetRole() == RoleType.Scp106 || p.GetRole() == RoleType.Scp096)
                    {
                        continue;
                    }
                    if (Vector3.Distance(Door.transform.position, p.GetPosition()) <= Global.distanceGate)
                    {
                        if (p.GetTeam() != Team.SCP || p.GetRole() == RoleType.Scp93953 || p.GetRole() == RoleType.Scp93989)
                            p.gameObject.GetComponent<CharacterClassManager>().RpcPlaceBlood(p.GetPosition(), 2, 3.0f);
                        p.playerStats.HurtPlayer(new PlayerStats.HitInfo(99999, p.nicknameSync.Network_myNickSync, DamageTypes.Wall, p.GetPlayerId()), p.gameObject);
                        p.ClearBroadcasts();
                        p.Broadcast(10, "<color=#ff0000>Вас раздавило гермоворотами</color>", true);
                    }
                }
                Destroy(gameObject.GetComponent<DoorAndPlayerTimeCheck>());
            }
        }
    }
}