using UnityEngine;

namespace Enerlion
{
    public class CellComponent : MonoBehaviour
    {
        public Transform Element;

        public bool GetElement()
        {
            return (Element == null);
        }

        // функция на удаление ?
    }
}