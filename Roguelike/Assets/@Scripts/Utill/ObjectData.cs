using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Object/ObjectData")]
public class ObjectData : ScriptableObject
{
    public int ID;
    public int atk;
    public int def;
    public int maxHp;
    public float speed;
    public int dropJam;
}
