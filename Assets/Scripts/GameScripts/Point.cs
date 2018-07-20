using UnityEngine;

namespace Enerlion
{
    public class Point : MonoBehaviour
    {
        public Vector3 Position
        {
            get { return transform.position; }
        }
    }
}
