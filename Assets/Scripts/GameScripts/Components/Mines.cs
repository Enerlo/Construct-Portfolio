using UnityEngine;

namespace Enerlion
{
    public class Mines : HPChanger
    {
        public GameObject Explouson;

        void OnDestroy()
        {
            Instantiate(Explouson);

        }
    }
}
