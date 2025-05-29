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
    public Transform[] StartDungeonPoint;
    public GameObject FireEffect;
    public GameObject DashEffect;
    public GameObject DoubleJumpEffet;
    public GameObject Scope;
    public ScopeController sController;
    public CinemachineCamera[] playerCam;
    public PolygonCollider2D[] mapCol;
    public CinemachineCamera scopeCam;

    public WeaponType weaponType;
    private float h;
    float riseHeight = 1.3f;
    float fallGravityScale = 11f;
    float lastInputTime = 0;
    float resetDelay = 0.5f;
    float jumpForce = 5;
    int jumpCount = 0;
    int comboCount = 0;
    public int magazineDrum = 5;
    bool canJump=true;
    public bool isAttacking = false;
    bool moveAble = true;
    public bool snipeMode = false;
    private Coroutine comboResetCoroutine;
    private Coroutine JumpCountCoroutine;
    int wType = 0;
    public int camPriority = 0;
    void Start()
    {
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
            anim.SetBool("onSnipe", true);
            Scope.SetActive(true);
        }
        else if (snipeMode == false)
        {
            Scope.transform.position = tr.position + new Vector3(1, 1, 0);
            anim.SetBool("onSnipe", false);
            Scope.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            collision.GetComponent<Point1>();
        }
    }
    public void UsePotal()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            tr.position = StartDungeonPoint[camPriority].position;
        }
       
    }
    IEnumerator DelayCamSwitch()
    {
        yield return new WaitForSeconds(1.5f);
        playerCam[camPriority].Priority = 30;
        playerCam[camPriority].GetComponent<CinemachineConfiner2D>().BoundingShape2D = mapCol[camPriority];
    }
    public void EnterSnipeMode()
    {
        moveAble = !moveAble;
        snipeMode = !snipeMode;
    }
    public void ExitScopeCam()
    {
        if (snipeMode == false)
        {
            playerCam[camPriority].Priority = 20;
            scopeCam.Priority = 0;
            magazineDrum = 5;
        }
    }
    public void EnterScopeCam()
    {
        if (snipeMode == true)
        {
            playerCam[camPriority].Priority = 10;
            scopeCam.Priority = 20;
        }
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
                tr.Translate(Time.deltaTime * 2  /*추후 플레이어 이동속도 변수로 받아오기 */* moveDir);
            }
            else
            {
                anim.SetBool(Define.isRunHash, false);
                tr.Translate(Time.deltaTime * 1  /*추후 플레이어 이동속도 변수로 받아오기 */* moveDir);
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
    /// 공중 공격 애니메이션 이벤트
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
    public void PlusComboCount() // 콤보 카운트 증가 Animation Event
    {
        comboCount++;
        if (comboCount > 2)
        {
            comboCount = 0;
        }
    }
    private void ResetComboCount() // 혹시 남아있는 comboCount 강제초기화
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

    public void ComboCountReset() // 콤보 카운트 리셋 AnimationEvent
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

            // 중간에 입력이 새로 들어왔으면 유지
            if (lastInputTime > lastCheckedInputTime)
            {
                comboResetCoroutine = null;
                yield break; // 콤보 유지
            }

            yield return null;
        }

        // 입력 없었으면 콤보 리셋
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
        ExitScopeCam();
        EnterScopeCam();
        ResetComboCount();
    }
}


// 일반 공격 하는 함수(f키만 눌렀을 때)

// 1번 콤보 공격까지 하는 함수(x키 && count 0)
// 
// == count 1
// == count 2 
/*
 총 제외 >> 칼 주먹 >> 콤보시스템임 >> 파라미터값을 공유함 anim.Trigger(skill), anim.SetInteger(0,1,2) << 이값은 스킬 사용중에 카운트가 올라감 0 >2  >>
스탠스마다 개별적으로 스킬 해금 시스템을 넣을건지? 
주먹 칼 이런거 다 파라미터를 공유함
 */

