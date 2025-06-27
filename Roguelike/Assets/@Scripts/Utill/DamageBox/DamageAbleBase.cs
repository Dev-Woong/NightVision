
public interface IDamageable
{
    public void TakeDamage(float Damage, WeaponType wType);
}

public abstract class DamageAbleBase : PoolAble, IDamageable 
{
    public bool damageAble = true;
    public void TakeDamage(float Damage,WeaponType wType)
    {
        if (damageAble == true)
        {
            OnDamage(Damage,wType);
        }
    }
    public abstract void OnDamage(float causerAtk,WeaponType wType);

}
