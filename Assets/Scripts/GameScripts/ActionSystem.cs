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
            Destroy(FindObjectOfType<InputEvent>().gameObject);
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
                            _player.HP = _hit.collider.gameObject.GetComponentInChildren<Element>().EnterCell(a, true);
                            return;
                        }
                    }
                }
            }
        }

        public void Shoot(bool isReload)
        {
            Destroy(FindObjectOfType<InputEvent>().gameObject);
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