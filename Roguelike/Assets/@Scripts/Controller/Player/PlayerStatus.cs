using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus: MonoBehaviour
{
    
    public float curEnergy;
    public float maxEnergy;
    public float EnergyRecovery;
    public Image Energy;
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
    private void Start()
    {
        curEnergy = maxEnergy;
    }
    void Update()
    {
        if (LoadingController.onInputBlocker == false)
        {
            EnergyCharge();
            EnergyAmount();
        }
    }
}
