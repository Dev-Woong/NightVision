using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public enum WeaponType
{
    Hand,
    Sword,
    Gun
}

public class PlayerController : MonoBehaviour
{
    Transform tr;
    Rigidbody2D rb;
    Animator anim;
    public Transform JumpEffectPoint;
    public Transform FirePoint;
    public GameObject FireEffect;
    public GameObject JumpEFfect;
    WeaponType weaponType;
    private float h;
    public float comboCountKeepTime = 0;
    public int comboCount = 0;
    public bool isJumping = false;
    public bool isAttacking = false;
    
    //public RuntimeAnimatorController handAnim;
    //public RuntimeAnimatorController SwordAnim;
    //public MonoBehaviour HandAnimScript;
    //public MonoBehaviour SwordAnimScript;
    //public MonoBehaviour GunAnimScript;
    public int wType = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        //HandAnimScript = gameObject.GetComponent<Anim_Hand>();
        //SwordAnimScript = gameObject.GetComponent <Anim_Sword>();
        //GunAnimScript = gameObject.GetComponent<Anim_Gun>();
        rb.freezeRotation = true;
    }
    void SetWeaponState()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            weaponType++;
            if (weaponType > WeaponType.Gun)
            {
                weaponType = WeaponType.Hand;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            weaponType--;
            if (weaponType < WeaponType.Hand)
            {
                weaponType = WeaponType.Gun;
            }
        }
        wType = (int)weaponType;
    }
    void GetWeaponState()
    {
        switch (weaponType)
        {
            case WeaponType.Hand:
               
                //HandAnimScript.enabled = true;
                //SwordAnimScript.enabled = false;
                //GunAnimScript.enabled = false;
                anim.SetInteger("WeaponType", wType);
            break;
            case WeaponType.Sword:
                //HandAnimScript.enabled = false;
                //SwordAnimScript.enabled = true;
                //GunAnimScript.enabled = false;
                anim.SetInteger("WeaponType", wType);
                break;
            case WeaponType.Gun:
                //HandAnimScript.enabled = false;
                //SwordAnimScript.enabled = false;
                //GunAnimScript.enabled = true;
                anim.SetInteger("WeaponType", wType);
                break;
        }
    }
    
    void Move()
    {
        if (Input.GetButton(Define.Horizontal) && (isAttacking == false || weaponType==WeaponType.Gun))
        {
            h = Input.GetAxisRaw(Define.Horizontal);
            Vector2 moveDir = new Vector2(h, 0);
            anim.SetBool(Define.isWalkHash, true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool(Define.isRunHash, true);
                tr.Translate(Time.deltaTime * 2  /*���� �÷��̾� �̵��ӵ� ������ �޾ƿ��� */* moveDir);
            }
            else
            {
                anim.SetBool(Define.isRunHash, false);
                tr.Translate(Time.deltaTime * 1  /*���� �÷��̾� �̵��ӵ� ������ �޾ƿ��� */* moveDir);
            }


            if (h > 0)
            {
                tr.localScale = new Vector3(1, 1, 1);
            }
            else if (h < 0)
            {
                tr.localScale = new Vector3(-1, 1, 1);
            }

        }
        else
        {
            anim.SetBool(Define.isWalkHash, false);
            anim.SetBool(Define.isRunHash, false);
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isAttacking = true;
            anim.SetBool(Define.isAttackHash, true);
            if (weaponType == WeaponType.Gun)
            {
                GameObject fireEffect = Instantiate(FireEffect, FirePoint);
            }
        }
    }
    void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (weaponType != WeaponType.Gun)
            {
                anim.SetBool(Define.isSkillHash, true);
                anim.SetInteger(Define.ComboCountHash, comboCount);
            }
        }
    }
    public void PlusComboCount() // Animation Event
    {
        comboCount++;
        if (comboCount > 2)
        {
            comboCount = 0;
        }
    }
    public void SkillHashFalse()
    {
        anim.SetBool(Define.isSkillHash, false);
        comboCount = 0;
    }
        
    public void AttackAnimFalse()
    {
        isAttacking = false;
        anim.SetBool(Define.isAttackHash, false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
        }
        
    }
    void Update()
    {
        Move();
        Attack();
        UseSkill();
        SetWeaponState();
        GetWeaponState();
    }
}
