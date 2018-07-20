using UnityEngine;

namespace Enerlion
{
    public class Core : MonoBehaviour, IDamage
    {
        private float _hp = 100;
        private bool _isDeath;


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
                if (_hp > 100) _hp = 100;
            }

            if (_hp <= 0)
            {
                _isDeath = true;
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