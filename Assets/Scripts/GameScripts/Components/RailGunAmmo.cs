
namespace Enerlion
{
    public class RailGunAmmo : Ammunation
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            SetDamage(collision.collider.GetComponent<IDamage>());
            Destroy(gameObject);
        }
    }
}
