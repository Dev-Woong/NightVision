using Microsoft.Win32.SafeHandles;
using System.Data.Common;
using UnityEngine;

public class PublicStatus : MonoBehaviour
{
    public ObjectData obData;
    public float atk;
    public float speed;
    public float maxHp;
    public float curHp;
    public float def;
    public int jamStack;
    void Start()
    {
        atk = obData.atk;
        speed = obData.speed;   
        maxHp = obData.maxHp;
        def = obData.def;
        jamStack = obData.dropJam;
        curHp = maxHp;
    }
    void StatusUpdate()
    {
        // TODO : �÷��̾� �����۱����ϸ� ���Ⱥ�ȭ�ֱ�
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
