using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Anim_SwordCombo3Spectrum : MonoBehaviour
{
    /// <summary>
    /// AnimationEvent Script
    /// </summary>
    public GameObject[] Combo3Spectrum;
    private Transform tr;
    public Transform finishAtkTr;
    
    public float s1PosX = 1.2f;
    public float s2PosX = 2.4f;
    public float s3PosX = 1.8f;
    public float s4PosX = 2.4f;
    public float s5PosX = 2f;
    public float s6PosX = 2.2f;
    public float s7PosX = 2;
   
    private void Start()
    {
        tr =GetComponent<Transform>();
    }
    public void Spect1() //좌 올려베기
    {
        Vector3 rightOffset = tr.position + new Vector3(s1PosX, 0, 0);
        Vector3 leftOffset = tr.position + new Vector3(-s1PosX, 0, 0);
        if (tr.localScale.x == 1)
        {
            
            Instantiate(Combo3Spectrum[0],rightOffset, Quaternion.identity);
        }
        else if (tr.localScale.x == -1)
        {
            
            Instantiate(Combo3Spectrum[0], leftOffset, Quaternion.identity);
        }
        
    }
    public void Spect2() // 우 올려베기
    {
        Vector3 rightOffset = tr.position + new Vector3(s2PosX, 0, 0);
        Vector3 leftOffset = tr.position + new Vector3(-s2PosX, 0, 0);
        
        if (tr.localScale.x == 1)
        {
            Instantiate(Combo3Spectrum[1], rightOffset, Quaternion.identity);
        }
        else if (tr.localScale.x == -1)
        {
            Instantiate(Combo3Spectrum[1],leftOffset, Quaternion.identity);
        }
    }
    public void Spect3()//중앙 횡베기
    {
        Vector3 rightOffset = tr.position + new Vector3(s3PosX, 0, 0);
        Vector3 leftOffset = tr.position + new Vector3(-s3PosX, 0, 0);
        
        if (tr.localScale.x == 1)
        {
            Instantiate(Combo3Spectrum[2], rightOffset, Quaternion.identity);
            
        }
        else if (tr.localScale.x == -1)
        {
            Instantiate(Combo3Spectrum[2],leftOffset, Quaternion.identity);
        }
    }
    public void Spect4() //좌 상 내려베기
    {
        Vector3 rightOffset = tr.position + new Vector3(s4PosX, 0, 0);
        Vector3 leftOffset = tr.position + new Vector3(-s4PosX, 0, 0);
        
        if (tr.localScale.x == 1)
        {
            Instantiate(Combo3Spectrum[3], rightOffset, Quaternion.identity);
        }
        else if (tr.localScale.x == -1)
        {
            Instantiate(Combo3Spectrum[3], leftOffset, Quaternion.identity);
        }
    }
    public void Spect5() //우 상 횡베기
    {
        Vector3 rightOffset = tr.position + new Vector3(s5PosX, 0, 0);
        Vector3 leftOffset = tr.position + new Vector3(-s5PosX, 0, 0);
        if (tr.localScale.x == 1)
        {
            Instantiate(Combo3Spectrum[4], rightOffset, Quaternion.identity);
        }
        else if (tr.localScale.x == -1)
        {
            Instantiate(Combo3Spectrum[4], leftOffset, Quaternion.identity);
        }
    }
    public void Spect6() //중앙 올려베기
    {
        Vector3 rightOffset = tr.position + new Vector3(s6PosX, 0, 0);
        Vector3 leftOffset = tr.position + new Vector3(-s6PosX, 0, 0);
        if (tr.localScale.x == 1)
        {
            Instantiate(Combo3Spectrum[5], rightOffset, Quaternion.identity);
        }
        else if (tr.localScale.x == -1)
        {
            Instantiate(Combo3Spectrum[5], leftOffset, Quaternion.identity);
        }
    }
    public void Spect7() //중 상 내려베기
    {
        Vector3 rightOffset = tr.position + new Vector3(s2PosX, 0.2f, 0);
        Vector3 leftOffset = tr.position + new Vector3(-s2PosX, 0.2f, 0);
        if (tr.localScale.x == 1)
        {
            Instantiate(Combo3Spectrum[6],rightOffset, Quaternion.identity);
        }
        else if (tr.localScale.x == -1)
        {
            Instantiate(Combo3Spectrum[6], leftOffset, Quaternion.identity);
        }
    }
    public void FinishAttack()
    {
        var FinishAttack = Instantiate(Combo3Spectrum[7]);
        if (tr.localScale.x == 1)
        {
            
            FinishAttack.transform.localScale = new Vector3(2.5f, 2.5f, 0);
        }
        else if (tr.localScale.x == -1)
        {
            FinishAttack.transform.localScale = new Vector3(-2.5f, 2.5f, 0);
        }
        FinishAttack.transform.position = finishAtkTr.position;
    }
    public void TransformLerp()
    {
        // 캐릭터 스워드 콤보3 애니메이션 재생 후 위치 보간
        if (tr.localScale.x == 1)
        {
            tr.position += new Vector3(0.28f, 0, 0); 
        }
        else if (tr.localScale.x == -1)
        {
            tr.position += new Vector3(-0.28f, 0, 0); 
        }
    }
}
