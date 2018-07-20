using UnityEngine;
using UnityEngine.AI;

namespace Enerlion
{
    public class Bot : MonoBehaviour, IDamage
    {

        private float _hp = 100;
        private bool _isDeath;

        public NavMeshAgent _agent;

        private float _curTime;
        private const float TIMEWAIT = 2;

        private Patrol _patrol;
        public bool IsHardPointPatrol;
        public Transform[] HardPoint;
        public Transform[] HealthPoint;

        private Transform _target;
        private bool _isPatrol = true;
        public Vision Vision;

        public Weapon Weapon;

        private int _currPoint;




        private void OnValidate()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _patrol = new Patrol();
            _target = GameObject.FindWithTag("Player").transform;
        }

        public float HP
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public void SetDamage(DamageInfo damage)
        {
            if (_hp > 0)
            {
                _hp -= damage.Damage;
                if (_hp <= 40)
                {
                    _agent.SetDestination(HealthPoint[0].position);
                    _agent.stoppingDistance = 0;
                }
            }

            if (_hp <= 0)
            {
                _isDeath = true;
                _agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;
                    var temRB = child.GetComponent<Rigidbody>();
                    if (!temRB)
                    {
                        temRB = child.gameObject.AddComponent<Rigidbody>();
                    }
                    temRB.AddForce(child.forward * Random.Range(1000, 5000));
                    Destroy(child.gameObject, 15);
                }
                _hp = 0;
            }
        }

        public void BotPatrul()
        {
            if (_isDeath) return;

            if (!IsHardPointPatrol)
            {
                if (_isPatrol)
                {
                    if (!_agent.hasPath)
                    {
                        _curTime += Time.deltaTime;
                        if (_curTime >= TIMEWAIT)
                        {
                            _curTime = 0;
                            _agent.SetDestination(_patrol.GenericPoint(_agent, false));
                            _agent.stoppingDistance = 0;
                        }
                    }

                    if (Vision.VisionMath(transform, _target))
                    {
                        _isPatrol = false;
                    }
                }
                else
                {
                    _agent.SetDestination(_target.position);
                    _agent.stoppingDistance = 2;


                    if (Vision.VisionMath(transform, _target))
                    {
                        Weapon.Shoot();
                    }
                    else
                    {
                        _isPatrol = true;
                        _agent.SetDestination(_patrol.GenericPoint(_agent, false));
                        _agent.stoppingDistance = 0;
                    }

                }
            }
            else
            {
                if (_isPatrol)
                {
                    if (!_agent.hasPath)
                    {
                        _curTime += Time.deltaTime;
                        if (_curTime >= TIMEWAIT)
                        {
                            _curTime = 0;
                            _agent.SetDestination(HardPoint[_currPoint].position);
                            _agent.stoppingDistance = 0;
                            if (_currPoint != HardPoint.Length - 1)
                            {
                                _currPoint++;
                                return;
                            }
                            _currPoint = 0;


                        }
                    }

                    if (Vision.VisionMath(transform, _target))
                    {
                        _isPatrol = false;
                    }
                }
                else
                {
                    _agent.SetDestination(_target.position);
                    _agent.stoppingDistance = 2;


                    if (Vision.VisionMath(transform, _target))
                    {
                        Weapon.Shoot();
                    }
                    else
                    {
                        _isPatrol = true;
                        _agent.SetDestination(HardPoint[_currPoint].position);
                        _agent.stoppingDistance = 0;
                        if (_currPoint != HardPoint.Length - 1)
                        {
                            _currPoint++;
                            return;
                        }
                        _currPoint = 0;
                    }

                }
            }
        }
    }
}