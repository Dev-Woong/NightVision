using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus: MonoBehaviour
{
    public float atk;
    public float def;
    public float speed = 1;
    public float curEnergy = 100;
    public float maxEnergy = 100;
    public Image Energy;
    void Start()
    {
        
    }

    public void EnergyAmount()
    {
        Energy.fillAmount = curEnergy / maxEnergy;
    }
    // Update is called once per frame
    void Update()
    {
        //EnergyAmount();
    }
}
