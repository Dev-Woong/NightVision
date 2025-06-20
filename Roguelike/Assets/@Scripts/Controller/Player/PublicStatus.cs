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
        // TODO : 플레이어 아이템구매하면 스탯변화주기
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
