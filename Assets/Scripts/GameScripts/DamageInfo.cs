namespace Enerlion
{
    public struct DamageInfo
    {
        readonly float _dmg;

        public DamageInfo(float damage)
        {
            _dmg = damage;
        }

        public float Damage
        {
            get { return _dmg; }
        }
    }
}
