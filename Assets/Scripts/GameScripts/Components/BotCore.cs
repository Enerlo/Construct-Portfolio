using UnityEngine;
using UnityEngine.AI;

namespace Enerlion
{
    public sealed class BotCore : Core
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _timeVait;
        [SerializeField] private bool IsRandomPatrol = false;
        [SerializeField] private Transform[] HardPoint;
        [SerializeField] private Transform[] HealthPoint;
        [SerializeField] private Vision Vision;
        [SerializeField] private Weapon Weapon;


        private float _currTime;
        private int _currPoint;
        private bool _isPatrol = true;
        private Transform _target;
        private Patrol _patrol;
        private AudioSource _alert;

        void OnValidate() 
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            _target = FindObjectOfType<PlayerCore>().transform;
            _patrol = new Patrol();
            _alert = GetComponent<AudioSource>();
        }

        public void BotPatrol()
        {
            if (_isDead) return;
            RandomPatrol();
        }

        void RandomPatrol()
        {
            if(_isPatrol)
            {
                if(!_agent.hasPath)
                {
                    _currTime += Time.deltaTime;
                    if (_currTime >= _timeVait)
                    {
                        _currTime = 0;
                        GetPatrol(IsRandomPatrol);
                    }

                }

                if (Vision.VisionMath(transform, _target))
                {
                    _isPatrol = false;
                    _alert.Play();
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
                    GetPatrol(IsRandomPatrol);
                }
            }
        }

        void GetPatrol(bool israndom)
        {
        var point = israndom ? _patrol.GenericPoint(_agent, false) :  HardPoint[_currPoint].position;
         _agent.SetDestination(point);
        _agent.stoppingDistance = 0;
            if (!israndom)
            {
                if (_currPoint != HardPoint.Length - 1)
                {
                    _currPoint++;
                    return;
                }
                _currPoint = 0;
            }
        }

        public override void SetDamage(DamageInfo damage)
        {
            if (_hp > 0)
            {
                _hp -= damage.Damage;
                if (_hp <= 40)
                {
                    foreach(var a in HealthPoint)
                    {
                        if(a.gameObject.GetComponent<GivenPlatform>().Shoose != null &&
                            a.gameObject.GetComponent<GivenPlatform>().Shoose == a.gameObject.GetComponent<GivenPlatform>().VariableObject[0])
                        {
                            _agent.SetDestination(a.position);
                            return;
                        }
                    }
                    _agent.stoppingDistance = 0;
                }
            }

            if (_hp <= 0)
            {
                _isDead = true;
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
    }
}