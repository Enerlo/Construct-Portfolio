using System.Collections.Generic;
using UnityEngine;

namespace Enerlion 
{
    class CoreCell : MonoBehaviour
    {
        private Transform _target;

        public List<CellComponent> Cells = new List<CellComponent>();

        void Start()
        {
            _target = FindObjectOfType<Core>().transform;
            var cells = FindObjectsOfType<CellComponent>();
            foreach (var a in cells)
            {
                Cells.Add(a);
                a.Parent = gameObject;
            }

        }

        public void CellMoving()
        {
            if (transform.position != _target.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, 10);
                Ray _ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
                transform.LookAt(_ray.direction * 1000);
            }
        }
    }
}
