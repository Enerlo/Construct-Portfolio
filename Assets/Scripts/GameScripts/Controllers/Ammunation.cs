using UnityEngine;

namespace Enerlion
{
    public abstract class Ammunation : MonoBehaviour
    {
        [SerializeField] protected float _bulletForse;
        [SerializeField] protected float _bulletLifetime;
        [SerializeField] protected float _bulletDamage;

        [HideInInspector]
        public float BulletForse
        {
            get { return _bulletForse; }
        }

        void Awake()
        {
            Destroy(gameObject, _bulletLifetime);
        }

        public void SetDamage(IDamage obj)
        {
            if (obj != null)
                obj.SetDamage(new DamageInfo(_bulletDamage));
        }
    }
}