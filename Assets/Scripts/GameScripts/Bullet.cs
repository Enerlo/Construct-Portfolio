using UnityEngine;

namespace Enerlion
{
    public class Bullet : MonoBehaviour
    {
        public int BullSpeed;
        public float LifetimeBull;
        public float Damage;


        void Awake()
        {
            Destroy(gameObject, LifetimeBull);
        }

        private void OnCollisionEnter(Collision collision)
        {
            SetDamage(collision.collider.GetComponent<IDamage>());
            Destroy(gameObject);
        }

        void SetDamage(IDamage obj)
        {
            if (obj != null) obj.SetDamage(new DamageInfo(Damage));
        }



    }
}
