
public interface IDamageable
{
    public void TakeDamage(float Damage);
}

public abstract class DamageAbleBase : PoolAble, IDamageable 
{
    public bool damageAble = true;
    public void TakeDamage(float Damage)
    {
        if (damageAble == true)
        {
            OnDamage(Damage);
        }
    }
    
    public abstract void OnDamage(float causerAtk);

}
