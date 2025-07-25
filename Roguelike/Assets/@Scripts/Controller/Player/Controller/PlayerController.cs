using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public enum WeaponType
{
    Hand,
    Sword,
    Gun
}


public class PlayerController :DamageAbleBase,IDamageable
{
    #region Component
    public ShopItemDatabase itemDatabase;
    Transform tr;
    Rigidbody2D rb;
    Animator anim;
    BoxCollider2D bc;
    public Transform JumpEffectPoint;
    public Transform FireEffectPoint;
    public Transform DashEffectPoint;
    public Transform ParringEffectPoint;
    public Transform ParringSpecPoint;
    public GameObject FireEffect;
    public GameObject DashEffect;
    public GameObject DoubleJumpEffet;
    public GameObject ParringEffect;
    public GameObject[] ParringSpectrum;
    public GameObject Scope;
    private PlayerStatus PlayerStat;
    private PublicStatus PublicStat;

    public Transform damagePos;
    public GameObject damageText;
    private RifleController rc;
    [SerializeField] private MapData home;
    readonly WaitForSeconds wTime = new(0.04f);
    private WeaponType weaponType;

    private float h;
    private float dashCoolTime;
    private readonly float riseHeight = 1.3f;
    private readonly float fallGravityScale = 11f;
    private float lastInputTime = 0;
    private readonly float resetDelay = 0.5f;
    private readonly float jumpForce = 5.5f;
    private int wType = 0;
    private int jumpCount = 0;
    private int comboCount = 0;
    private int mode = 0;
    private float maxHp;
    public float curHp;
    public int magazineDrum = 5;
    public int shopOpenCount = 1;

    public bool canDownJump = false;
    private bool canJump = true;
    private bool rifleMode = false;
    public bool modeSelection = false;
    public bool moveAble = true;
    public bool snipeMode = false;
    public bool isParring = false;
    public bool isAttacking = false;
    public Vector3 portalMovePosition;
    public Vector2 parringBCSize;
    public Vector2 normalBCSize;
    private Coroutine comboResetCoroutine;
    private Coroutine JumpCountCoroutine;

    public GameObject[] GunModes;
    public GameObject GunModePanel;
    public GameObject Rifle;
    public GameObject[] SwapEffect;
    public Transform SwapEffectPoint;

    CameraChanger camChanger;
    public AttackData normalGunAttack;
    public AudioClip dashSFX;
    public AudioClip jumpSFX;
    public AudioClip[] swapSFX;
    public AudioClip parringSFX;
    public SlicedFilledImage HPImage;
    public GameObject PlayerUI;
    #endregion


    public override void OnDamage(float causerAtk, WeaponType wType)
    {
        curHp -= causerAtk;
        GameObject hudText = Instantiate(damageText);
        hudText.transform.position = damagePos.position;
        hudText.GetComponent<DamageText>().damage = causerAtk;
        if (rifleMode == false && snipeMode == false)
        {
            anim.SetTrigger("Hurt");
            moveAble = false;
            isAttacking = false;
        }
        rb.gravityScale = 1.0f;
        if (curHp <= 0)
        {
            LoadingController.onInputBlocker = true;
            snipeMode = false;
            isAttacking = false;
            rifleMode = false;
            PlayerStat.curEnergy = PlayerStat.maxEnergy;
            rb.linearVelocity = Vector3.zero;
            rb.gravityScale = 0;
            StartCoroutine(Die());
        }
    }
    
    public void HPProcess()
    {
        HPImage.fillAmount = curHp/PublicStat.maxHp;
    }
    #region GunMode
    public void EnterSnipeMode()
    {
        if (snipeMode == true)
        {
            anim.SetBool("onSnipe", true);
            Scope.SetActive(true);
        }
        else if (snipeMode == false)
        {
            Scope.transform.position = tr.position + new Vector3(1, 1, 0);
            Scope.SetActive(false);
            anim.SetBool("onSnipe", false);
        }
    }
    public void ExitSnipeMode()
    {
        PublicStat.knockBack = KnockBack.Done;
        snipeMode = !snipeMode;
        isAttacking = false;
        moveAble = true;
    }
    public void MoveAbleFalse()
    {
        isAttacking = true;
    }
    public void OnMoveAble() // AnimationEvent
    {
        isAttacking = false ;
    }
    public void EnterRifleMode()
    {
        anim.SetBool("onRifle", rifleMode);
        Rifle.SetActive(rifleMode);
    }
    public void ExitRifleMode()
    {
        PublicStat.knockBack = KnockBack.Done;
        rifleMode = false;
        isAttacking = false;
        moveAble = true;
        anim.SetBool("onRifle", rifleMode);
    }
    public void EnterGunMode()
    {
        modeSelection = !modeSelection;
    }
    public void SelectGunMode()
    {
        if (modeSelection == true)
        {
            rb.gravityScale = 1;
            if (Input.GetKeyDown(KeyCode.D))
            {
                mode++;
                if (mode >= 2)
                {
                    mode = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (PlayerStat.curEnergy >= 60)
                {
                    PlayerStat.curEnergy -= 60;
                    switch (mode)
                    {
                        case 0:
                            isAttacking = true;
                            PublicStat.knockBack = KnockBack.None;
                            modeSelection = false;
                            snipeMode = true;
                            moveAble = false;
                            mode = 0;
                            break;
                        case 1:
                            isAttacking = true;
                            PublicStat.knockBack = KnockBack.None;
                            modeSelection = false;
                            rifleMode = !rifleMode;
                            mode = 0;
                            moveAble = false;
                            StartCoroutine(RifleFire());
                            break;
                    }
                }
                else return;
            }
        }
    }
    IEnumerator RifleFire()
    {
        yield return wTime;
        rc.Fire();
    }
    public void GunModeUI()
    {
        GunModePanel.SetActive(modeSelection);

        if (mode == 0)
        {
            GunModes[0].SetActive(true);
            GunModes[1].SetActive(false);
        }
        else if (mode == 1)
        {
            GunModes[0].SetActive(false);
            GunModes[1].SetActive(true);
        }
        else return;
    }
    #endregion
    #region Weapon
    void SetWeaponState()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SFXManager.Instance.PlaySFX(swapSFX[0]);
            Instantiate(SwapEffect[0],SwapEffectPoint);
            weaponType = WeaponType.Hand;
            comboCount = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SFXManager.Instance.PlaySFX(swapSFX[1]);
            Instantiate(SwapEffect[1], SwapEffectPoint);
            weaponType = WeaponType.Sword;
            comboCount = 0;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SFXManager.Instance.PlaySFX(swapSFX[2]);
            Instantiate(SwapEffect[2], SwapEffectPoint);
            weaponType = WeaponType.Gun;
            comboCount = 0;
        }
        wType = (int)weaponType;
        
        if (weaponType != WeaponType.Gun) 
        {
            modeSelection = false; GunModePanel.SetActive(false);
        }
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
    #endregion
    #region PlayerController
    void Move()
    {
        if (Input.GetButton(Define.Horizontal) && isAttacking == false )
        {
            h = Input.GetAxisRaw(Define.Horizontal);
            Vector2 moveDir = new Vector2(h, 0);
            anim.SetBool(Define.isWalkHash, true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool(Define.isRunHash, true);
                tr.Translate(Time.deltaTime * PublicStat.speed * 2 * moveDir);
            }
            else
            {
                anim.SetBool(Define.isRunHash, false);
                tr.Translate(Time.deltaTime * PublicStat.speed * moveDir);
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
        dashCoolTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)&& dashCoolTime <=0 && rifleMode == false  && snipeMode == false)
        {
            SFXManager.Instance.PlaySFX(dashSFX);
            isAttacking = false;
            moveAble = true;
            rb.linearVelocity = Vector3.zero;
            anim.SetTrigger(Define.DashHash);
            rb.gravityScale = 1;
            if (tr.localScale.x == 1)
            {
                DashEffect.transform.localScale = new Vector3(1, 0, 0);
            }
            else if (tr.localScale.x == -1)
            {
                DashEffect.transform.localScale = new Vector3(-1, 0, 0);
            }
            Instantiate(DashEffect, DashEffectPoint);
            dashCoolTime = 0.2f;
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A) && canJump == true)
        {
            if (weaponType != WeaponType.Gun)
            {
                StartCoroutine(CoAttack());
            }
            else anim.SetTrigger("Attack");
        }
    }
    IEnumerator CoAttack()
    {
        anim.SetBool("isWalk", false);
        anim.SetBool("isRun", false);
        yield return null;
        anim.SetTrigger("Attack");
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.05f);
        
            anim.SetTrigger("Die");
            moveAble = false;
            damageAble = false;
        yield return null;
    }
    public void Respawn()
    {
        if (LoadingSceneManager.respawnAble == true)
        {
            anim.SetTrigger("Respawn");
            moveAble = true;
            damageAble = true;
            rb.gravityScale = 1;
            curHp = PublicStat.maxHp;
            shopOpenCount = 1;
            transform.position = home.SpawnPoint;
            LoadingSceneManager.respawnAble = false;
        }
    }
    private void DieProcess() // AnimationEvent
    {
        UIManager.Instance.PlayerDiePanel();
    }
    public void Parring()
    {
        if (Input.GetKeyDown(KeyCode.D) && isParring == false&&weaponType == WeaponType.Sword&&PlayerStat.curEnergy >= 10)
        {
            bc.size = parringBCSize;
            anim.SetTrigger("Parring");
            PlayerStat.curEnergy -= 10;                                         
            isParring = true;
            rb.gravityScale = 1f;
            moveAble = false;
        }
    }
    public void ExitParring()
    {
        bc.size = normalBCSize;
        isParring = false;
        moveAble = true;
    }
    void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.S)&&PlayerStat.curEnergy >=15 && canJump == true)
        {
            StartCoroutine(CoSkillAttack());
        }
    }
    IEnumerator CoSkillAttack()
    {
        lastInputTime = Time.time;
        anim.SetBool("isWalk", false);
        anim.SetBool("isRun", false);
        if (weaponType != WeaponType.Gun)
        {
            if (comboResetCoroutine != null)
            {
                StopCoroutine(comboResetCoroutine);
                comboResetCoroutine = null;
            }
            isAttacking = true;
            anim.SetTrigger(Define.useSkillHash);
            anim.SetInteger(Define.comboCountHash, comboCount);
        }
        else
        {
            if (moveAble == true && isAttacking == false)
            {
                EnterGunMode();
            }
        }
        yield return null;
    }
    public void UseEnergy()
    {
        PlayerStat.curEnergy -= 15;
    }
    #endregion
    public void OnAirTool()
    {
        anim.SetBool("onAir", true);
    }
    #region Jump
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (canDownJump == false)
            {
                if (jumpCount == 0 && canJump == true)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.gravityScale = 1;
                    canJump = false;
                    SFXManager.Instance.PlaySFX(jumpSFX);
                    anim.SetTrigger("Jump");
                    rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                    if (JumpCountCoroutine != null)
                    {
                        StopCoroutine(JumpCountCoroutine);
                    }
                    JumpCountCoroutine = StartCoroutine(DoubleJumpCoroutine());
                }
            }
            else if (canDownJump == true&&onSecondFloor ==true)
            {
                rb.gravityScale = 1;
                canJump = false;
                StartCoroutine(DownJumpCoroutine());
                moveAble = true;
                SFXManager.Instance.PlaySFX(jumpSFX);
                anim.SetTrigger("Jump");
                rb.AddForce(0.5f * jumpForce * Vector3.down, ForceMode2D.Impulse);
                if ( JumpCountCoroutine !=null)
                {
                    StopCoroutine(JumpCountCoroutine);
                }
                JumpCountCoroutine = StartCoroutine(DoubleJumpCoroutine());
            }
        }
    }
    void DownJump()
    {
        canDownJump = Input.GetKey(KeyCode.DownArrow);
    }
    IEnumerator DownJumpCoroutine()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("UpStair"),true);
        bc.enabled = false;
        yield return new WaitForSeconds(0.3f);
        bc.enabled = true;
        onSecondFloor = false;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("UpStair"), false);
        
    }
    IEnumerator DoubleJumpCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        jumpCount++;
    }
    void DoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.C) && jumpCount == 1)
        {
            SFXManager.Instance.PlaySFX(jumpSFX);
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
    public void MoveAble() // Animation Event
    {
        moveAble = true;
    }

    #endregion
    #region AnimationEvent
    public void GunEffect() // AnimEvent
    {
        GameObject fireEffect = Instantiate(FireEffect, FireEffectPoint.position, Quaternion.identity);

        if (transform.localScale.x == 1)
        { fireEffect.transform.eulerAngles = new Vector3(0, 0, 0); }
        else if (transform.localScale.x == -1)
        {
            fireEffect.transform.eulerAngles = new Vector3(0, 0, 180);
        }
    }
    public void DashPositionChange()
    {
        if (tr.localScale.x == 1)
        { tr.position += new Vector3(1, 0, 0) * 0.3f; }

        else if (tr.localScale.x == -1)
        { tr.position += new Vector3(-1, 0, 0) * 0.3f; }
    }

    public void OnAirComboStart()
    {
        rb.gravityScale = 0;
        rb.linearVelocity = Vector3.zero;
        tr.position += Vector3.up * riseHeight;
        anim.SetBool("onAir", true);
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
        Vector3 fallForce = Vector3.down;
        rb.AddForce(fallForce * 10, ForceMode2D.Impulse);
    }
    public void PlusComboCount()
    {
        comboCount++;
        if (comboCount > 2)
        {
            comboCount = 0;
        }
    }
    private void ResetComboCount() 
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

            if (lastInputTime > lastCheckedInputTime)
            {
                comboResetCoroutine = null;
                yield break;
            }

            yield return null;
        }
        comboCount = 0;
        comboResetCoroutine = null;
    }
    #endregion
    bool onSecondFloor = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {

            if (collision.gameObject.layer != LayerMask.NameToLayer("UpStair"))
            {
                canJump = true;
                jumpCount = 0;
                anim.SetBool("onAir", false);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("UpStair"))
            {
                if (collision.gameObject.transform.position.y <= transform.position.y+0.1f)
                {
                    onSecondFloor = true;
                    canJump = true;
                    jumpCount = 0;
                    anim.SetBool("onAir", false);
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("UpStair"))
        {
            onSecondFloor = false;
        }
        if (collision.collider.CompareTag("Ground"))
        {
            anim.SetBool("onAir", false);
        }
    }
    
    public void ParringSpectrumProcess()
    {
        int a = Random.Range(0, 6);
        var dd = Instantiate(ParringSpectrum[a], ParringSpecPoint.position, Quaternion.identity);
        dd.transform.localScale = transform.localScale;
        var parringEffect = Instantiate(ParringEffect, ParringEffectPoint.position, Quaternion.identity);
        parringEffect.transform.localScale = transform.localScale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.GetComponent<Bullet>().targetMask == 6)
            {
                if (isParring == true)
                {
                    int a = Random.Range(0, 6);
                    SFXManager.Instance.PlaySFX(parringSFX);
                    anim.SetTrigger("EnterBullet");
                    anim.SetInteger("RandomParring", a);
                    other.GetComponent<Rigidbody2D>().linearVelocity = -other.GetComponent<Rigidbody2D>().linearVelocity*1.5f;
                    other.GetComponent<Bullet>().atk += PublicStat.atk * 10f;
                    other.GetComponent<Bullet>().targetMask = LayerMask.NameToLayer("Enemy");
                }
                else
                {
                    if (curHp > 0) 
                    {
                        anim.SetTrigger("Hurt");
                        float bulletAtk = other.gameObject.GetComponent<Bullet>().atk;
                        OnDamage(bulletAtk, weaponType);
                        other.gameObject.SetActive(false);
                    }
                }
            }
        }
        var portal = other.GetComponent<MapPortal>();

        if (portal == null)
        {
            return;
        }
        
        var nextMapData = portal.targetMapData;
        if (nextMapData == null)
        {
            return;
        }
        if (nextMapData != null)
        {
            StartCoroutine(HandleMapTransition(nextMapData));
        }
    }

    void InitializeCamAndItem(MapData mapData)
    {
        camChanger.Initialize();
        ShopManager.Instance.NewShopItems(itemDatabase, 4);
    }
    IEnumerator HandleMapTransition(MapData targetMapData)
    {
        yield return null;
        // 위치 선정
        GetComponent<PlayerPositionManager>().SetTargetSpawnId(targetMapData.spawnPointId);
        // 씬 로딩
        LoadingSceneManager.LoadScene(targetMapData.sceneName,targetMapData);

        LoadingController.onInputBlocker = true;
        // 초기화 코루틴 실행
        bool load = false;
        while (load  == false)
        {
            if (targetMapData.useInitializeCamAndItem==true&& LoadingSceneManager.onLoadScene==true)
            {
                yield return new WaitForSeconds(7f);
                LoadingController.onInputBlocker = false;
                yield return new WaitForSeconds(2f);
                InitializeCamAndItem(targetMapData);
                load = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    #region LifeCycle
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        PlayerStat = GetComponent<PlayerStatus>();
        PublicStat = GetComponent<PublicStatus>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rc = GetComponentInChildren<RifleController>();
        bc = GetComponent<BoxCollider2D>();
        
        
        normalBCSize = bc.size;
        
        parringBCSize = new Vector2(2.5f, 0.9f);
    }
    
    void Start()
    {
        Rifle.SetActive(false);
        Scope.SetActive(false);
        curHp = PublicStat.maxHp;
        rb.freezeRotation = true;

        camChanger = GetComponent<CameraChanger>();
    }
    
    void Update()
    {
        if (LoadingController.onInputBlocker == false && LoadingController.onOpenShop == false && LoadingController.onPause == false)
        {
            if (moveAble == true)
            {
                Move();
                Jump();
                SetWeaponState();
                GetWeaponState();
                DownJump();
                Parring();
                Attack();
                UseSkill();
            }
            GunModeUI();
            SelectGunMode();
            Dash();
            EnterRifleMode();
            DoubleJump();
            OnAir();
            EnterSnipeMode();
            ResetComboCount();
        }
        HPProcess();
        PlayerUI.SetActive(!LoadingController.onInputBlocker);
        Respawn();
    }
    #endregion
}




