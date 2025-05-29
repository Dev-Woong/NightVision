using System.Data;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public AttackData aData;

    public void CreateAttackBox()
    {
        Vector2 center = (Vector2)transform.position;

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, aData.hitBoxSize, 0, aData.targetMask);
        foreach (Collider2D hit in hits)
        {
            
        }
    }
}
