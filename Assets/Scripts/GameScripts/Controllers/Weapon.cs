using UnityEngine;

namespace Enerlion
{
    public abstract class Weapon : Element
    {
        [SerializeField] protected int _countBullet;
        protected int _currentBullet;
        [SerializeField] protected float _shootTime;
        [SerializeField] protected float _reloadTime;
        [SerializeField] protected Transform _barrel;
        [SerializeField] protected Ammunation _bullet;
        [SerializeField] protected AudioSource _audioShoot;

        private bool _isFire = true;

        public int CurrettBullet
        {
            get { return _currentBullet; }
        }

        void Awake()
        {
            _currentBullet = _countBullet;
            _audioShoot = GetComponent<AudioSource>();
        }

        public void Shoot()
        {
            if (_currentBullet == 0 || !_isFire) return;

            _isFire = false;
            var shoot = Instantiate(_bullet, _barrel.position, Quaternion.identity);
            shoot.GetComponent<Rigidbody>().AddForce(_barrel.forward * _bullet.BulletForse);
            _currentBullet--;
            _audioShoot.Play();

            Invoke("IsFire", _shootTime);
        }

        public void Reload()
        {
            _isFire = false;
            Invoke("IsFire", _reloadTime);
            _currentBullet = _countBullet;
        }

        void IsFire()
        {
            _isFire = true;
        }
    }
}