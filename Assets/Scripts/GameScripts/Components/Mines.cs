using UnityEngine;

namespace Enerlion
{
    class Mines : HPChanger
    {
        public GameObject Explouson;

        private void OnDestroy()
        {

            Instantiate(Explouson);
        }
    }
}
