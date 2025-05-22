using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/AttackSkill")]
public class SkillData : ScriptableObject   
{
    [Header("��ų ID")]
    public string skillID;
    [Header("������ (��� ������ ���� ����)")]
    public float damage;
    [Header("��ų ��Ʈ��")]
    public int hitCount;
    [Header("���� ���� ����")]
    public Vector2 hitBoxSize;
    [Header("��Ʈ�ڽ� ���� ��ġ ����")]
    public float rangeOffset;
    
}
