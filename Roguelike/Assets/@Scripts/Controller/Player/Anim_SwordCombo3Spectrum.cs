using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Anim_SwordCombo3Spectrum : MonoBehaviour
{
    /// <summary>
    /// AnimationEvent Script
    /// </summary>
    public GameObject[] Combo3Spectrum;
    public Transform tr;
    
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
    public void Spect1() //�� �÷�����
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
    public void Spect2() // �� �÷�����
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
    public void Spect3()//�߾� Ⱦ����
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
    public void Spect4() //�� �� ��������
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
    public void Spect5() //�� �� Ⱦ����
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
    public void Spect6() //�߾� �÷�����
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
    public void Spect7() //�� �� ��������
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
    public void TransformLerp()
    {
        // ĳ���� ������ �޺�3 �ִϸ��̼� ��� �� ��ġ ����
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
