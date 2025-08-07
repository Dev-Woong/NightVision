
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus: MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }
    public PublicStatus status;
    public float curEnergy;
    public float maxEnergy;
    public float atk;
    public float speed;
    public float maxHp;
    public int def;
    public int jamStack;
    public float EnergyRecovery;
    public bool onInvincibleMode = false;
    public float curInvincibleTriggerTime = 0;
    public Image Energy;
    public GameObject CheatImageBar;
    public TMP_Text CheatText;
    public GameObject JamUI;
    public TMP_Text JamText;
    public void EnergyAmount()
    {
        Energy.fillAmount = curEnergy / maxEnergy;
    }
    public void EnergyCharge()
    {
        if (curEnergy <= maxEnergy)
        {
            curEnergy += Time.deltaTime * EnergyRecovery;
        }
    }
    public void FilledCheatImage()
    {
        
        CheatImageBar.GetComponent<Image>().fillAmount = curInvincibleTriggerTime / 3;
    }
    public void InvincibleModeTrigger()
    {
        if (Input.GetKey(KeyCode.P)&&onInvincibleMode ==false)
        {
            CheatImageBar.SetActive(true);
            CheatText.text = "치트모드 활성화중,,,";
            curInvincibleTriggerTime += Time.deltaTime;
            if (curInvincibleTriggerTime >3)
            {
                onInvincibleMode = true;
                CheatText.text = "실행 대기중. p를 한번 더 눌러주세요.";
            }
        }
        if (Input.GetKeyUp(KeyCode.P) && onInvincibleMode == false)
        {
            curInvincibleTriggerTime = 0;
            CheatImageBar.SetActive(false);
        }
    }
    IEnumerator CheatTextController()
    {
        CheatText.text = "치트 모드 활성화.";
        yield return new WaitForSeconds(1.5f);
        CheatImageBar.SetActive(false);
    }
    public void OnInvincibleMode()
    {
        if (Input.GetKeyDown(KeyCode.P)&&onInvincibleMode == true)
        {
            maxHp = float.MaxValue;
            EnergyRecovery = 100;
            jamStack = 99999999;
            status.SetItemStats(this);
            UpdateJam();
            StartCoroutine(CheatTextController());
        }
    }

    public void SetItemStats(ShopItemSlot item)
    {
        atk += item.atk;
        speed += item.speed;
        maxHp += item.maxHp;
        def += item.def;
        maxEnergy += item.energy;
        EnergyRecovery += item.energyRecovery;  
        jamStack -= item.price;
        status.SetItemStats(this);
        UpdateJam();
    }
    public void UpdateJam()
    {
        JamText.text = jamStack.ToString();
    }
    
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void Start()
    {
        curEnergy = maxEnergy;
        status = GetComponent<PublicStatus>();
        CheatImageBar.SetActive(false);
        UpdateJam();
    }
    void Update()
    {
        if (LoadingController.onInputBlocker == false)
        {
            EnergyCharge();
            EnergyAmount();
            InvincibleModeTrigger();
            OnInvincibleMode();
            FilledCheatImage();
        }
        JamUI.SetActive(!LoadingController.onInputBlocker);
    }
}
