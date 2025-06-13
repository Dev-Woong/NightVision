using UnityEngine;

public class Player_Effect : MonoBehaviour
{
    public GameObject DownSlash;
    public GameObject UpSlash;
    public GameObject HorizonSlash;
    public GameObject TeleportEffect;
    public Transform DownSlashPos;
    public Transform UpSlashPos;
    public Transform HorizonSlashPos;
    public Transform TeleportEffectPos;
    public void DownSlashEffect()
    {
        Instantiate(DownSlash,DownSlashPos);
    }
    public void UpSlashEffect()
    {
        Instantiate (UpSlash,UpSlashPos);
        
    }
    public void HorizonSlashEffect()
    {
        Instantiate(HorizonSlash, HorizonSlashPos);
    }
    public void Combo3TeleportEffect()
    {
        Instantiate(TeleportEffect, TeleportEffectPos);
    }
    
}
