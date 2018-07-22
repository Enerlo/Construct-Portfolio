using UnityEngine;

namespace Enerlion
{
    public class CellComponent : MonoBehaviour
    {
        public Transform Element;
        public Transform FocusGun;

        public void Awake()
        {
            transform.LookAt(FocusGun);
        }

        public bool GetElement()
        {
            return (Element == null);
        }

        // функция на удаление ?
    }
}