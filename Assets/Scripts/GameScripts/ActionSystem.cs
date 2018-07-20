using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enerlion
{
    public class ActionSystem : MonoBehaviour
    {
        private CoreCell _coreCell;
        private Camera _camera;
        private Ray _ray;
        private RaycastHit _hit;


        private void Start()
        {
            _coreCell = FindObjectOfType<CoreCell>().GetComponent<CoreCell>();
            _camera = FindObjectOfType<Camera>();
        }

        public void Action()
        {
            Destroy(FindObjectOfType<InputEvent>().gameObject);
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, 1))
                if (_hit.collider.tag == "Element")
                {
                    foreach (var cell in _coreCell.Cells)
                    {
                        if (cell.Element == null && _hit.collider.gameObject.GetComponentInChildren<Element>().Parent == null && cell.IsSpecialCell != true)
                        {
                            AddElement(_hit.collider.gameObject, cell);
                            Destroy(_hit.collider.gameObject);
                            return;
                        }
                    }
                }
        }

        void AddElement(GameObject element, CellComponent cell)
        {
            cell.Element = Instantiate(element);
            cell.Element.GetComponentInChildren<Element>().Parent = cell.transform;
            cell.Element.transform.parent = cell.transform;
            cell.Element.transform.position = cell.transform.position;
            cell.Element.transform.localRotation = cell.transform.localRotation;
            //Destroy(cell.Element.GetComponent<Rigidbody>());
        }

        public void Shoot()
        {
            Destroy(FindObjectOfType<InputEvent>().gameObject);
            foreach (var cell in _coreCell.Cells)
                if (cell.Element != null)
                    if (cell.Element.GetComponentInChildren<Weapon>() == true)
                        cell.Element.GetComponentInChildren<Weapon>().Shoot();
        }

        public void Reload()
        {
            Destroy(FindObjectOfType<InputEvent>().gameObject);
            foreach (var cell in _coreCell.Cells)
                if (cell.Element != null)
                    if (cell.Element.GetComponentInChildren<Weapon>() == true)
                        cell.Element.GetComponentInChildren<Weapon>().Reload();
        }
    }
}