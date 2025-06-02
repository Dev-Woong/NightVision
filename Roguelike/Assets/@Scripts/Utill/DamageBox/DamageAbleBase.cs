
public interface IDamageable
{
    public void TakeDamage(float Damage);
}

public abstract class DamageAbleBase : PoolAble, IDamageable 
{
    public bool canDamage;
    public virtual void TakeDamage(float Damage)
    {
        if (canDamage == false) return;
        OnDamage(Damage);
    }
    public abstract void OnDamage(float causerAtk); 
}
