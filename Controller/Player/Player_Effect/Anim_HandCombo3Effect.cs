using UnityEngine;

public class Anim_HandCombo3Effect : MonoBehaviour
{
    public GameObject LeftEffect;
    public GameObject RightEffect;
    
    public void Effect()
    {
        Vector3 leftOffset = transform.position + new Vector3(0.5f, 0, 0);
        Vector3 rightOffset = transform.position + new Vector3(-0.5f, 0, 0);
        Instantiate(LeftEffect,leftOffset,Quaternion.identity);
        Instantiate(RightEffect,rightOffset, Quaternion.identity);
    }
}
