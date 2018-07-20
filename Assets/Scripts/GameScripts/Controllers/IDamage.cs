namespace Enerlion
{
    public interface IDamage
    {
        float HP { get; }

        void SetDamage(DamageInfo damage);
    }
}
