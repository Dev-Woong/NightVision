using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/AttackSkill")]
public class AttackData : ScriptableObject   
{

    public LayerMask targetMask;

    public WeaponType weaponType;

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
