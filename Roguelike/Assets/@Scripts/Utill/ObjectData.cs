using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Object/ObjectData")]
public class ObjectData : ScriptableObject
{
    public string ID;
    
    public float moveSpeed;
    public int atk;
    public int def;
    public int maxHp;
    public float speed;
    public int dropJam;
    
}
