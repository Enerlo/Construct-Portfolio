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
        [SerializeField] private Vision Vision;
        [SerializeField] private Weapon Weapon;

        private float _currTime;
        private int _currPoint;
        private bool _isPatrol = true;
        private Transform _target;
        private Patrol _patrol;

        void OnValidate() 
        {
            _agent = GetComponent<NavMeshAgent>();
        }

         void Start()
        {
            _target = FindObjectOfType<PlayerCore>().transform;
            _patrol = new Patrol();
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
                }

            }
            else
            {
                _agent.SetDestination(_target.position);
                _agent.stoppingDistance = 2;
                if (Vision.VisionMath(transform, _target))
                    Weapon.Shoot();
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
    }
}