using UnityEngine;
public interface IDamageable
{
    public void TakeDamage(float Damage);
}

public abstract class DamageAbleBase : MonoBehaviour, IDamageable 
{
    public bool canDamage;
    public void TakeDamage(float Damage)
    {
        if (canDamage == false) return;
        OnDamage(Damage);
    }
    public abstract void OnDamage(float causerAtk); 
}
