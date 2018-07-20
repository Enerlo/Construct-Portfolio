using UnityEngine;

namespace Enerlion
{
    public class CellComponent : MonoBehaviour
    {
        public Transform Element;
        //private Transform _parent;


        //void Awake()
        //{
        //    _parent = GetComponentInParent<Core>().transform;
        //}

        public bool GetElement()
        {
            return (Element == null);
        }

        // функция на удаление
    }
}