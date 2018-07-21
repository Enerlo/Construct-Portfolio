using UnityEngine;

namespace Enerlion
{
    public class PlayerControllSystem : MonoBehaviour
{
    [SerializeField] private float _movePower = 5;
    [SerializeField] private bool _useTorque = true;
    [SerializeField] private float _maxAngularVelocity = 25;
    [SerializeField] private float _jumpPower = 2;

    private Transform _camera;
    private Rigidbody _rigidbody;
    private GameObject _core;
    private Vector3 _move;
    private Vector3 _camforward;
    private bool _jump;

    private const float _groundRayLength = 1f;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>().transform;
    }

    private void Start()
    {
        _core = FindObjectOfType<PlayerCore>().gameObject;
        _rigidbody = _core.GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = _maxAngularVelocity;
    }


    //ОСтавляю до лучших времен, когда научусь прикреплять к движущимся объектам неподвижные точки внутри
    public void Move()
    {
       // Destroy(FindObjectOfType<InputEvent>().gameObject);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _jump = Input.GetButton("Jump");
            if (h != 0 || v != 0 || _jump)
            {
                _camforward = Vector3.Scale(_camera.forward, new Vector3(1, 0, 1)).normalized;
                _move = (v * _camforward + h * _camera.right).normalized;
                Move(_move, _jump);
                _jump = false;
            }

    }
        void Move(Vector3 moveDirection, bool jump)
        {
            if (_useTorque)
            {
                _rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * _movePower);
            }
            else
                _rigidbody.AddForce(moveDirection * _movePower);

            if (Physics.Raycast(_core.transform.position, -Vector3.up, _groundRayLength) && jump)
                _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }
}