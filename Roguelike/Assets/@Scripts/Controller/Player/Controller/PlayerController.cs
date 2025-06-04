using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

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
    public GameObject DashEffect;
    public GameObject DoubleJumpEffet;
    public GameObject Scope;
    public PlayerStatus ps;
    public CinemachineBrain brain;
    WaitForSeconds wTime = new(0.04f);
    public WeaponType weaponType;
    private float h;
    float riseHeight = 1.3f;
    float fallGravityScale = 11f;
    float lastInputTime = 0;
    float resetDelay = 0.5f;
    float jumpForce = 5;
    int jumpCount = 0;
    public int comboCount = 0;
    public int magazineDrum = 5;
    bool canJump=true;
    bool moveAble = true;
    public bool snipeMode = false;
    public Vector3 portalMovePosition;
    private Coroutine comboResetCoroutine;
    private Coroutine JumpCountCoroutine;
    int wType = 0;
    Color ScopeColor= new Color32(0,0,0,255);
    
    void Start()
    {
        ps = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        Scope.SetActive(false);
        rb.freezeRotation = true;
    }
   
    public void ScopeC()
    {
        if (snipeMode == true)
        {
            
            brain.DefaultBlend.Style = CinemachineBlendDefinition.Styles.EaseInOut;
            anim.SetBool("onSnipe", true);
            StartCoroutine(CineBrainBlendInOut());
        }
        else if (snipeMode == false)
        {
           
            Scope.transform.position = tr.position + new Vector3(1, 1, 0);
            Scope.SetActive(false);
            anim.SetBool("onSnipe", false);
            StartCoroutine(CineBrainBlendCut());
        }
    }
    IEnumerator CineBrainBlendInOut()
    {
        yield return wTime;
        Scope.SetActive(true);
    }
    IEnumerator CineBrainBlendCut()
    {
        yield return wTime;
        brain.DefaultBlend.Style = CinemachineBlendDefinition.Styles.Cut;
    }
    public void EnterSnipeMode()
    {
        moveAble = !moveAble;
        snipeMode = !snipeMode;
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
                anim.SetInteger("WeaponType", wType);
            break;
            case WeaponType.Sword:
                anim.SetInteger("WeaponType", wType);
                break;
            case WeaponType.Gun:
                anim.SetInteger("WeaponType", wType);
                break;
        }
    }
    
    void Move()
    {
        if (Input.GetButton(Define.Horizontal) /* && (isAttacking == false || weaponType==WeaponType.Gun)*/)
        {
            h = Input.GetAxisRaw(Define.Horizontal);
            Vector2 moveDir = new Vector2(h, 0);
            anim.SetBool(Define.isWalkHash, true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool(Define.isRunHash, true);
                tr.Translate(Time.deltaTime * ps.speed*2* moveDir);
            }
            else
            {
                anim.SetBool(Define.isRunHash, false);
                tr.Translate(Time.deltaTime * ps.speed* moveDir);
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
    
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
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
            else 
            {
                EnterSnipeMode();
            }
        }
    }
    #region Jump
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (jumpCount == 0&&canJump==true)
            {
                canJump = false;
               
                anim.SetTrigger("Jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                if (JumpCountCoroutine != null)
                {
                    StopCoroutine (JumpCountCoroutine);
                }
                JumpCountCoroutine = StartCoroutine(DoubleJumpCoroutine());
                
            }
        }
    }
    IEnumerator DoubleJumpCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        jumpCount++;
    }
    void DoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && jumpCount == 1)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            Instantiate(DoubleJumpEffet, JumpEffectPoint.position, Quaternion.identity);
            anim.SetTrigger("DoubleJump");
            jumpCount++;
        }
    }
    
    public void OnAir()
    {
        if (canJump == false)
        {
            anim.SetBool("onAir", true);
        }
        
    }
    
    
    #endregion
    #region AnimationEvent
    public void DashPositionChange()
    {
        if (tr.localScale.x == 1)
        { tr.position += new Vector3(1, 0, 0) * 0.3f; }

        else if (tr.localScale.x == -1)
        { tr.position += new Vector3(-1, 0, 0) * 0.3f; }
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
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            canJump = true;
            jumpCount = 0;
            anim.SetBool("onAir", false); 
        }
        if (collision.collider.CompareTag("UpStair"))
        {
            canJump = true;
            ps.speed = 2;
            jumpCount = 0;
            anim.SetBool("onAir", false);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedTool"))
        {
            ps.speed = 1;
        }
        
    }
    
    private void FixedUpdate()
    {
        //Landing();
    }
    void Update()
    {
        if( moveAble == true) 
        { 
            Move(); 
            Jump();
            Dash();
            SetWeaponState();
            GetWeaponState();
            Attack();
        }

        DoubleJump();
        OnAir();
        ScopeC();
        UseSkill();
        ResetComboCount();
    }
}




