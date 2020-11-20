using Exiled.API.Features;
using UnityEngine;

namespace DoorStuck
{
    public class DoorAndPlayerTimeCheck : MonoBehaviour
    {
        private float TimeIsUp = 0.1f;
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
            if (!IsGate && !Door.DoorName.Contains("173"))
            {
                foreach (Door door in Map.Doors)
                {
                    if (door.DoorName == "173")
                    {
                        if (Vector3.Distance(Door.gameObject.transform.position, door.gameObject.transform.position) < 20.0f)
                            IsGate = true;
                        break;
                    }
                }
            }
            if (!IsGate)
            {
                foreach (Door door in Map.Doors)
                {
                    if (door.DoorName.ToLower().Contains("049") && door.DoorName.ToLower().Contains("armory") && door.DoorName != Door.DoorName)
                    {
                        if (Vector3.Distance(door.gameObject.transform.position, Door.gameObject.transform.position) < 15.0f)
                            IsGate = true;
                        break;
                    }
                }
            }
            if (IsGate)
                TimeIsUp = 1.6f;
        }

        public void Update()
        {
            Timer += Time.deltaTime;
            
            if (Timer > TimeIsUp)
            {
                foreach (Player p in Player.List)
                {
                    if (!IsGate && p.Team == Team.SCP)
                        continue;
                    if (p.Role == RoleType.Spectator || p.Team == Team.TUT || p.Role == RoleType.Scp079 || p.Role == RoleType.Scp106)
                        continue;
                    if (IsGate)
                    {
                        if (Vector3.Distance(Door.transform.position, p.Position) <= Global.DistanceGate)
                        {
                            if (p.Team != Team.SCP || p.Role == RoleType.Scp93953 || p.Role == RoleType.Scp93989)
                                p.GameObject.GetComponent<CharacterClassManager>().RpcPlaceBlood(p.Position, 2, 3.0f);
                            p.Hurt(999999, p, DamageTypes.Wall);
                            p.ClearBroadcasts();
                            p.Broadcast(10, "<color=#ff0000>Вас раздавило гермоворотами</color>", Broadcast.BroadcastFlags.Monospaced);
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(Door.transform.position, p.Position) <= Global.Distance)
                        {
                            p.Hurt(10, p, DamageTypes.Wall);
                            p.ClearBroadcasts();
                            p.Broadcast(10, "<color=#ff0000>Вас прищемило дверью</color>", Broadcast.BroadcastFlags.Monospaced);
                        }
                    }
                }
                Destroy(gameObject.GetComponent<DoorAndPlayerTimeCheck>());
            }
        }
    }
}