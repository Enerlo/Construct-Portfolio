using UnityEngine;
namespace Enerlion
{
    public class CameraControllSystem : MonoBehaviour
    {
        private Transform _player;

        private Transform _camera;
        private Transform _pivot;
        private Vector3 _lastTargetPosition;

        private float _lookAngle;
        private float _titleAngle;
        private Vector3 _pivotEluers;
        private Quaternion _pivotTargetRot;
        private Quaternion _transtormTargetRot;

        private const float _lookDistance = 50f;

        private Cam _camProp;

        private void Awake()
        {
            _camera = FindObjectOfType<Camera>().transform;
            _camProp = _camera.GetComponent<Cam>();

            _pivot = _camera.parent;
            _pivotEluers = _pivot.rotation.eulerAngles;

            _transtormTargetRot = _camera.parent.parent.transform.localRotation;
        }

        private void Start()
        {
            _player = FindObjectOfType<PlayerCore>().transform;
            if (_player == null) return;
        }

        public void HandleRotationMovement()
        {
            if (Time.timeScale < float.Epsilon)
                return;

            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");

            _lookAngle += x * _camProp.TurnSpeed;
            _transtormTargetRot = Quaternion.Euler(0, _lookAngle, 0);

            _titleAngle -= y * _camProp.TurnSpeed;
            _titleAngle = Mathf.Clamp(_titleAngle, -_camProp.TiltMIN, _camProp.TiltMAX);

            _pivotTargetRot = Quaternion.Euler(_titleAngle, _pivotEluers.y, _pivotEluers.z);

            if (_camProp.TurnSmoothing > 0)
            {
                _pivot.localRotation = Quaternion.Slerp(_pivot.localRotation, _pivotTargetRot, _camProp.TurnSmoothing * Time.deltaTime);
                _pivot.parent.localRotation = Quaternion.Slerp(_pivot.parent.localRotation, _transtormTargetRot, _camProp.TurnSmoothing * Time.deltaTime);
            }
            else
            {
                _pivot.localRotation = _pivotTargetRot;
                _pivot.parent.localRotation = _transtormTargetRot;
            }
        }

        public void FollowTarget(float deltaTime)
        {
            if (_player == null) return;
            _pivot.parent.position = Vector3.Lerp(_pivot.parent.position, _player.position, deltaTime * _camProp.CameraSpeed);
        }
    }
}