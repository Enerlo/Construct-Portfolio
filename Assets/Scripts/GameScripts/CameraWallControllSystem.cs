using System;
using UnityEngine;

namespace Enerlion
{
    public class CameraWallControllSystem : MonoBehaviour
    {
        private float _clipMoveTime = 0.01f;
        private float _returnTime = 0.3f;
        private float _sphereCastRadius = 0.1f;
        private float _closestDistance = 0.5f;
        [HideInInspector] public bool Protecting { get; private set; }
        private string _dontClipTag = "Element";

        private CameraControllSystem _cameraControll;

        private Transform _camera;
        private Transform _pivot;
        private float _originalDistance;
        private float _moveVelocity;
        private float _currentDistance;

        private Ray _ray = new Ray();
        private RaycastHit[] _hits;
        private RayHitComparer _rayHitComparer;

        private void Start()
        {
            _camera = FindObjectOfType<Camera>().transform;
            _pivot = _camera.parent;

            _originalDistance = _camera.localPosition.magnitude;
            _currentDistance = _originalDistance;

            _rayHitComparer = new RayHitComparer();
        }

        public void CameraWall()
        {
            float targetDist = _originalDistance;

            _ray.origin = _pivot.position + _pivot.forward * _sphereCastRadius;
            _ray.direction = -_pivot.forward;

            var cols = Physics.OverlapSphere(_ray.origin, _sphereCastRadius);

            bool initialIntersect = false;
            bool hitSomething = false;

            for (int i = 0; i < cols.Length; i++)
            {
                if ((!cols[i].isTrigger) && !(cols[i].attachedRigidbody != null && cols[i].attachedRigidbody.CompareTag(_dontClipTag)))
                {
                    initialIntersect = true;
                    break;
                }
            }


            if (initialIntersect)
            {
                _ray.origin += _pivot.forward * _sphereCastRadius;
                _hits = Physics.RaycastAll(_ray, _originalDistance - _sphereCastRadius);
            }
            else
            {
                _hits = Physics.SphereCastAll(_ray, _sphereCastRadius, _originalDistance + _sphereCastRadius);
            }

            Array.Sort(_hits, _rayHitComparer);

            float nearest = Mathf.Infinity;

            for (int i = 0; i < _hits.Length; i++)
            {
                if (_hits[i].distance < nearest && (!_hits[i].collider.isTrigger) && !(_hits[i].collider.attachedRigidbody != null && _hits[i].collider.attachedRigidbody.CompareTag(_dontClipTag)))
                {
                    nearest = _hits[i].distance;
                    targetDist = -+_pivot.InverseTransformPoint(_hits[i].point).z;
                    hitSomething = true;
                }
            }

            if (hitSomething)
            {
                Debug.DrawRay(_ray.origin, -_pivot.forward * (targetDist + _sphereCastRadius), Color.red);
            }
            Protecting = hitSomething;
            _currentDistance = Mathf.SmoothDamp(_currentDistance, targetDist, ref _moveVelocity, _currentDistance > targetDist ? _clipMoveTime : _returnTime);
            _currentDistance = Mathf.Clamp(_currentDistance, _closestDistance, _originalDistance);
            _camera.localPosition = -Vector3.forward * _currentDistance;
        }

    }
}