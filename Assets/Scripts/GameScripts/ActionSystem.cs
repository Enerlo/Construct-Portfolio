using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enerlion
{
    public class ActionSystem : MonoBehaviour
    {
        private Camera _camera;
        private Ray _ray;
        private RaycastHit _hit;
        private PlayerCore _player;


        private void Start()
        {
            _camera = FindObjectOfType<Camera>();
            _player = FindObjectOfType<PlayerCore>();
        }
        
        public void Action()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, 1))
            {
                if (_hit.collider.tag == "Element")
                {
                    var cells = _player.GetCellInfo();
                    foreach (var a in cells)
                    {
                        bool b1 = a.GetElement();
                        bool b2 = _hit.collider.gameObject.GetComponentInChildren<Element>().GetElement();
                        if (b1 && b2)
                        {
                            //нужно лучшее решение
                            AddElement(_hit.collider.gameObject, a);
                            Destroy(_hit.collider.gameObject);
                            return;
                        }
                    }
                }
            }
        }

        void AddElement(GameObject element, CellComponent cell)
        {
            cell.Element = Instantiate(element).transform;
            cell.Element.parent = cell.transform;
            cell.Element.position = cell.transform.position;
            cell.Element.localRotation = cell.transform.localRotation;
            Destroy(cell.Element.GetComponent<Rigidbody>());
        }


        public void Shoot(bool isReload)
        {
            var cells = _player.GetCellInfo();

            foreach(var a in cells)
            {
                if (a.Element != null && a.Element.GetComponentInChildren<Weapon>() == true)
                {
                    if (!isReload)
                        a.Element.GetComponentInChildren<Weapon>().Shoot();
                    else
                        a.Element.GetComponentInChildren<Weapon>().Reload();
                }
                    
            }
        }
    }
}