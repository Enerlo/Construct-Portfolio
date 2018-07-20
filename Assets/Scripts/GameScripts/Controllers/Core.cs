using UnityEngine;

namespace Enerlion
{
    public abstract class Core : MonoBehaviour, IDamage
    {
        [SerializeField] protected float _hp;
        [SerializeField] protected bool _isDead;
        [SerializeField] protected CellComponent[] _cells;
        [SerializeField] private GameObject _explosive;

        public float HP
        {
            get { return _hp; }
            set { _hp = value; }
        }


        void Awake()
        {
            _cells = GetComponentsInChildren<CellComponent>();
        }

        public void SetDamage(DamageInfo damage)
        {
            if( _hp <= 0)
            {
                _isDead = true;
                DecompositeBody();
            }
            else
            {
                _hp = _hp > 100 ? _hp = 100 : _hp -= damage.Damage; 
            }
        }

        void DecompositeBody()
        {
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                child.parent = null;

                if (!child.GetComponent<Rigidbody>())
                    child.gameObject.AddComponent<Rigidbody>();

                var rb = child.GetComponent<Rigidbody>();
                rb.AddForce(child.forward * Random.Range(100, 1000));

                Instantiate(_explosive, child.position, Quaternion.identity);
                Destroy(child.gameObject, 10);
            }
        }
    }
}