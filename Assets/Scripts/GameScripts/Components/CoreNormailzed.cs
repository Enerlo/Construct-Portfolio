using UnityEngine;

namespace Enerlion
{
    public class CoreNormailzed : MonoBehaviour
    {
        private Transform _target;

        void Start()
        {
            _target = FindObjectOfType<PlayerCore>().transform;
        }

        public void CellMoving()
        {
            if (transform.position == _target.position) return;

            transform.position = Vector3.MoveTowards(transform.position, _target.position, 10);
            Ray _ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
            transform.LookAt(_ray.direction * 1000);
        }
    }
}