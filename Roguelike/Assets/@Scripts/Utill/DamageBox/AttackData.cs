using UnityEngine;




[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/AttackSkill")]
public class AttackData : ScriptableObject   
{

    [Header("Ÿ��")]
    public LayerMask targetMask;

    [Header("���� Ÿ��")]
    public WeaponType weaponType;

    [Header("��ų �ҷ����� ����")]
    [Tooltip("�ִϸ��̼� �̺�Ʈ ���� ������ ����")]
    [TextArea]
    public string animSpot;

    [Header("��ų ��Ʈ��")]
    public int hitCount;

    [Header("������ (��� ������ ���� ����)")]
    public float damageValue;

    //[Header("���� ����Ʈ")]
    //public AudioClip SFX;

    [Header("Ÿ�� ����Ʈ")]
    public GameObject HitEffect;
    [Header("Ÿ�� ����Ʈ ���� ��ġ")]
    public Vector3 effectPos;

    [Header("���� ���� ����")]
    public Vector3 hitBoxSize;

    [Header("��Ʈ�ڽ� ���� ��ġ ����")]
    public float rangeOffset;
}
