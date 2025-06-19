using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyID;
    
    public float moveSpeed;
    public int enemyAtk;
    public int enemyHp;
    
    public int dropJam;
    
}
