using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class PublicStatus : MonoBehaviour
{
    public ObjectData obData;
    public PlayerController pc;

    public float atk;
    public float speed;
    public float maxHp;
    public float def;
    public float baseAtk;
    public float baseSpeed;
    public float baseMaxHp;
    public int baseDef;
    public float itemAtk;
    public float itemSpeed;
    public float itemMaxHp;
    public int itemDef;
    public int dropJam;
    public KnockBack knockBack;
    public bool checkDie = false;
    void Awake()
    {
        baseAtk = obData.atk;
        baseSpeed = obData.speed;
        baseMaxHp = obData.maxHp;
        baseDef = obData.def;
        dropJam = obData.dropJam;
        pc = GetComponent<PlayerController>();
    }
    private void Start()
    {
        atk = baseAtk;
        speed = baseSpeed;
        maxHp = baseMaxHp;
        def = baseDef;
    }
    public void StatusUpdate()
    {
        atk = baseAtk +itemAtk;
        speed =baseSpeed+ itemSpeed;
        maxHp = baseMaxHp+itemMaxHp;
        def = baseDef+itemDef;
        StartCoroutine(SetHp());
    }
    IEnumerator SetHp()
    {
        yield return null;
        pc.curHp = maxHp;
    }
    public void SetItemStats(PlayerStatus ps)
    {
        itemAtk = ps.atk;
        itemSpeed = ps.speed;
        itemMaxHp = ps.maxHp;
        itemDef = ps.def;
        StatusUpdate();
    }
}
