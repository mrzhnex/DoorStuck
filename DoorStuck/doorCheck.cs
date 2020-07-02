using UnityEngine;

namespace DoorStuck
{
    class doorCheck : MonoBehaviour
    {
        private float timer = 0f;
        public void Update()
        {
            if (global.openDoors != null)
            {
                foreach (Door d in global.openDoors)
                {
                    if (!d.isOpen)
                        if (d.gameObject.GetComponent<doorAndPlayerTimeCheckcs>() == null)
                            d.gameObject.AddComponent<doorAndPlayerTimeCheckcs>();
                }
            }
            timer = timer + Time.deltaTime;
            if (timer > 0.3f)
            {
                timer = 0f;
                global.OpenDoorsSet();
            }
        }
    }
}
