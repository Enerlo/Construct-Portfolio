using UnityEngine;

namespace Enerlion
{
    public class Weapon : Element
    {
        //public string WeaponName;
        public int CountBullet;
        private int CurrentBullet;
        public float TimeBetweenShoot;
        public float ReloadTime;

        public Transform Barrel;
        public Bullet Bull;

        private bool _isFire = true;

        private void Awake()
        {
            CurrentBullet = CountBullet;
        }

        public  void Shoot()
        {
            if (CurrentBullet == 0 || !_isFire) return;

            _isFire = !_isFire;
            var shoot = Instantiate(Bull, Barrel.position, Quaternion.identity);
            shoot.GetComponent<Rigidbody>().AddForce(Barrel.forward * Bull.BullSpeed);
            CurrentBullet--;
            Invoke("IsFire", TimeBetweenShoot);
        }

        public void Reload()
        {
            _isFire = false;
            Invoke("IsFire", ReloadTime);
            CurrentBullet = CountBullet;
        }

        void IsFire()
        {
            _isFire = true;
        }
    }
}