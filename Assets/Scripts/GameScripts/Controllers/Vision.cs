using System;
using UnityEngine;

namespace Enerlion
{
    [Serializable]
    public class Vision
    {
        public float ActivDist = 20;
        public float ActivAngle = 30;

        public bool VisionMath(Transform player, Transform target)
        {
            return Dis(player, target) && Angle(player, target) && !CheckBloked(player, target);
        }

        private bool CheckBloked(Transform player, Transform target)
        {
            RaycastHit hit;
            if (!Physics.Linecast(player.position, target.position, out hit)) return true;
            return hit.transform != target;
        }

        private bool Angle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(player.forward, target.position - player.position);
            return angle <= ActivAngle;
        }

        private bool Dis(Transform player, Transform target)
        {
            var dist = Vector3.Distance(player.position, target.position);
            return dist <= ActivDist;
        }
    }
}
