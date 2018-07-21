using UnityEngine;

namespace Enerlion
{
    public abstract class Element : MonoBehaviour
    {
        public float _healthToCell;
        protected Transform _parent;

        public abstract void UltimateSkill();

        public bool GetElement()
        {
            return (_parent == null);
        }
    }
}
