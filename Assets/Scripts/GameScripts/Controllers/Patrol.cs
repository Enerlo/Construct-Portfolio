using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enerlion
{
    public class Patrol
    {
        private int _minDis = 10;
        private int _maxDis = 50;
        private List<Vector3> _points = new List<Vector3>();
        private int _currentPoint;

        public Patrol()
        {
            var tempPoints = GameObject.FindObjectsOfType<Point>();

            _points = tempPoints.Select(point => point.Position).ToList();
        }


        public Vector3 GenericPoint(NavMeshAgent agent, bool isRandom = true)
        {
            if (isRandom)
            {
                var dist = Random.Range(_minDis, _maxDis);
                var randomPoint = Random.insideUnitSphere * dist;
                NavMeshHit hit;
                NavMesh.SamplePosition(agent.transform.position + randomPoint, out hit, dist, NavMesh.AllAreas);
                return hit.position;
            }
            else
            {
                if (_currentPoint < _points.Count - 1)
                {
                    _currentPoint++;
                }
                else
                {
                    _currentPoint = 0;
                }
                return _points[_currentPoint];
            }
        }
    }
}
