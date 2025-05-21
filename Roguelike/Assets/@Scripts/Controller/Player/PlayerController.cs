using System.Collections;
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
    public Transform FireEffectPoint;
    public Transform DashEffectPoint;
    public GameObject FireEffect;
    public GameObject JumpEFfect;
    public GameObject DashEffect;

    WeaponType weaponType;
    private float h;
    public float comboCountKeepTime = 0;
    public float riseHeight = 0.8f;
    public float fallGravityScale = 11f;
    public float lastInputTime = 0;
    public float startCoroutineTime = 0;
    public float resetDelay = 0.5f;
    public int comboCount = 0;
    public bool isJumping = false;
    public bool isAttacking = false;
    private Coroutine comboResetCoroutine;
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
            comboCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            weaponType--;
            if (weaponType < WeaponType.Hand)
            {
                weaponType = WeaponType.Gun;
            }
            comboCount = 0;
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
        if (Input.GetButton(Define.Horizontal)/* && (isAttacking == false || weaponType==WeaponType.Gun)*/)
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
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(Define.DashHash);
            rb.gravityScale = 1;
            if (tr.localScale.x == 1)
            {
                DashEffect.transform.localScale = new Vector3(1,0,0);
                
            }
            else if (tr.localScale.x == -1)
            {
                DashEffect.transform.localScale = new Vector3(-1, 0, 0);
               
            }
            Instantiate(DashEffect, DashEffectPoint);
        }
    }
    public void DashPositionChange()
    {
        if (tr.localScale.x == 1)
        { tr.position += new Vector3(1, 0, 0) * 0.3f; }

        else if (tr.localScale.x == -1)
        { tr.position += new Vector3(-1, 0, 0) * 0.3f; }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isAttacking = true;
            //anim.SetBool(Define.isAttackHash, true);
            anim.SetTrigger("Attack");
            if (weaponType == WeaponType.Gun)
            {
                GameObject fireEffect = Instantiate(FireEffect, FireEffectPoint);
            }
        }
    }
    void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            lastInputTime = Time.time;
            if (weaponType != WeaponType.Gun)
            {
                if (comboResetCoroutine != null)
                {
                    StopCoroutine(comboResetCoroutine);
                    comboResetCoroutine = null;
                }
                anim.SetTrigger(Define.useSkillHash);
                anim.SetInteger(Define.comboCountHash, comboCount);
            }
        }
    }
    /// <summary>
    /// ���� ���� �ִϸ��̼� �̺�Ʈ
    /// </summary>
    public void OnAirComboStart()
    {
        rb.gravityScale = 0;
        rb.linearVelocity = Vector3.zero;
        tr.position += Vector3.up * riseHeight;
    }
    public void OnAirComboHold()
    {
        rb.gravityScale = 0;
        rb.linearVelocity = Vector3.zero;
    }
    public void OnAirComboFall()
    {
        rb.gravityScale = fallGravityScale;
    }
    public void OnAirComboFinish()
    {
        rb.gravityScale = 1;
        
    }
    public void Combo3PositionChange()
    {
        if (tr.localScale.x == 1)
        { tr.position += new Vector3(1, 0, 0) * 4; }

        else if (tr.localScale.x == -1)
        { tr.position += new Vector3(-1, 0, 0) * 4; }
    }
    
    public void OnHandCombo3Fall()
    {
        rb.linearVelocity = Vector3.zero;
        Vector3 fallForce = new Vector3(tr.localScale.x, -1, 0);
        rb.AddForce(fallForce * 10, ForceMode2D.Impulse);
    }
    public void PlusComboCount() // �޺� ī��Ʈ ���� Animation Event
    {
        comboCount++;
        if (comboCount > 2)
        {
            comboCount = 0;
        }
    }
    private void ResetComboCount() // Ȥ�� �����ִ� comboCount �����ʱ�ȭ
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Player_Hand_Idle") && comboCount != 0)
        {
            comboCount = 0;
            anim.SetInteger(Define.comboCountHash, 0);
        }
        else if (state.IsName("Player_Sword_Idle") && comboCount != 0)
        {           
            comboCount = 0;
            anim.SetInteger(Define.comboCountHash, 0);
        }
    }

    public void ComboCountReset() // �޺� ī��Ʈ ���� AnimationEvent
    {
        if (comboResetCoroutine != null)
        {
            StopCoroutine(comboResetCoroutine);
        }

        comboResetCoroutine = StartCoroutine(ComboCountZero());
    }
    IEnumerator ComboCountZero()
    {
        float timer = 0f;
        float lastCheckedInputTime = lastInputTime;

        while (timer < resetDelay)
        {
            timer += Time.deltaTime;

            // �߰��� �Է��� ���� �������� ����
            if (lastInputTime > lastCheckedInputTime)
            {
                comboResetCoroutine = null;
                yield break; // �޺� ����
            }

            yield return null;
        }

        // �Է� �������� �޺� ����
        comboCount = 0;
        comboResetCoroutine = null;
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
        Dash();
        Attack();
        UseSkill();
        ResetComboCount();
        SetWeaponState();
        GetWeaponState();
    }
}
