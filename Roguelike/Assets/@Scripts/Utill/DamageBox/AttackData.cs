using UnityEngine;

public enum Target 
{
    Enemy,
    Player
}
[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/AttackSkill")]
public class AttackData : ScriptableObject   
{
    [Header("타겟")]
    public Target Target;
    [Header("스킬 불러오는 시점")]
    [TextArea]
    [Tooltip("애니메이션 이벤트를 어느 시점에 넣어야 하는지 설명 (예: 타격 직전 프레임)")]
    public string animSpot;
    [Header("불러오는 ID")]
    public string skillID;
    [Header("데미지 (계산 공식은 추후 설정)")]
    public float damage;
    [Header("스킬 히트수")]
    public int hitCount;
    [Header("공격 범위 설정")]
    public Vector3 hitBoxSize;
    [Header("히트박스 생성 위치 설정")]
    public float rangeOffset;
    [Header("타겟")]
    public LayerMask targetMask;
}
