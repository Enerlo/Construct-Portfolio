using UnityEngine;

namespace Enerlion
{
    public abstract class Element : MonoBehaviour
    {
        [SerializeField] protected float _healthToCell;
        protected Transform _parent;

        public abstract void UltimateSkill();

        public bool GetElement()
        {
            return (_parent == null);
        }

        public float EnterCell(CellComponent cell, bool isEnter)
        {
            if (isEnter)
            {
                transform.parent = cell.transform;
                cell.Element = transform;
                _parent = cell.transform;
                return _healthToCell;
            }
            else
            {
                transform.parent = null;
                cell.Element = null;
                _parent = null;
                return _healthToCell * (-1);
            }
        }
    }
}
