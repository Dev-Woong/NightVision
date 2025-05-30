using System.Data;
using UnityEngine;

public class DamageHandler : MonoBehaviour 
{
    public AttackData aData;

    public void SpawnHitbox()
    {
        Vector3 spawnPos = transform.position + transform.right * aData.rangeOffset;
        Collider2D[] hits = Physics2D.OverlapBoxAll(spawnPos, aData.hitBoxSize, 0f, aData.targetMask);

        foreach (Collider2D hit in hits)
        {
            IDamageable target = hit.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(aData.damage);
            }
        }
    }

    // 히트박스 확인용 (디버깅용 Gizmos)
    private void OnDrawGizmosSelected()
    {
        if (aData == null) return;

        Gizmos.color = Color.red;
        Vector3 spawnPos = transform.position + transform.right * aData.rangeOffset;
        Gizmos.DrawWireCube(spawnPos, aData.hitBoxSize);
    }
}
