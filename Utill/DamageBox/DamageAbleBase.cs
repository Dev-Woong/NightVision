
public interface IDamageable
{
    public void TakeDamage(float Damage);
}

public abstract class DamageAbleBase : PoolAble, IDamageable 
{
    public void TakeDamage(float Damage)
    {
        OnDamage(Damage);
    }
    
    public abstract void OnDamage(float causerAtk);

}
