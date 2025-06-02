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
    public Target target;

    [Header("무기 타입")]
    public WeaponType weaponType;

    [Header("스킬 불러오는 시점")]
    [Tooltip("애니메이션 이벤트 생성 프레임 시점")]
    [TextArea]
    public string animSpot;

    [Header("스킬 히트수")]
    public int hitCount;

    [Header("데미지 (계산 공식은 추후 설정)")]
    public float damageValue;

    [Header("사운드 이펙트")]
    public AudioClip SFX;

    [Header("타격 이펙트")]
    public GameObject HitEffect;

    [Header("공격 범위 설정")]
    public Vector3 hitBoxSize;

    [Header("히트박스 생성 위치 설정")]
    public float rangeOffset;

    [Header("타겟")]
    public LayerMask targetMask;
}
