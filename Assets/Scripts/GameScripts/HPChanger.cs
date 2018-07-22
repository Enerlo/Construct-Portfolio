using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enerlion
{
    public class HPChanger : MonoBehaviour
    {
        public float ChangeHP;

        private void OnCollisionEnter(Collision collision)
        {
            var a = collision.collider.GetComponent<IDamage>();
            a.SetDamage(new DamageInfo(ChangeHP * (-1)));

            Destroy(gameObject);
        }
    }
}