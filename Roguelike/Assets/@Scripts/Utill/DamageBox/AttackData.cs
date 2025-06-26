using UnityEngine;

public enum SkillType 
{
    Normal,
    AOE
}
public enum KnockBack
{
    None,
    Done
}

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/AttackSkill")]
public class AttackData : ScriptableObject   
{
    public WeaponType wType;

    public LayerMask targetMask;

    public KnockBack knockBack;

    public float knockBackForceX;
    public float knockBackForceY;
    public int getEnergy;

    public SkillType skillType; 

    public string animSpot;

    public int hitCount;

    public float damageValue;

    //[Header("사운드 이펙트")]
    //public AudioClip SFX;

    public GameObject HitEffect;

    public Vector3 effectPos;

    public Vector3 hitBoxSize;

    public float rangeOffset;
}
