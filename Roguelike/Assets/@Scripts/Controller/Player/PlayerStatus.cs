
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
    public Image Energy;
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
        UpdateJam();
    }
    void Update()
    {
        if (LoadingController.onInputBlocker == false)
        {
            EnergyCharge();
            EnergyAmount();
        }
        JamUI.SetActive(!LoadingController.onInputBlocker);
    }
}
