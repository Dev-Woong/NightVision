using UnityEngine;

public enum Target 
{
    Enemy,
    Player
}
[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/AttackSkill")]
public class AttackData : ScriptableObject   
{
    [Header("Ÿ��")]
    public Target Target;
    [Header("��ų �ҷ����� ����")]
    [TextArea]
    [Tooltip("�ִϸ��̼� �̺�Ʈ�� ��� ������ �־�� �ϴ��� ���� (��: Ÿ�� ���� ������)")]
    public string animSpot;
    [Header("�ҷ����� ID")]
    public string skillID;
    [Header("������ (��� ������ ���� ����)")]
    public float damage;
    [Header("��ų ��Ʈ��")]
    public int hitCount;
    [Header("���� ���� ����")]
    public Vector3 hitBoxSize;
    [Header("��Ʈ�ڽ� ���� ��ġ ����")]
    public float rangeOffset;
    [Header("Ÿ��")]
    public LayerMask targetMask;
}
