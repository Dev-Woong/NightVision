using UnityEngine;

[CreateAssetMenu(menuName = "MapData")]
public class MapData : ScriptableObject
{
    [Header("다음 씬 명")]
    public string sceneName; // 씬 명
    [Header("다음 씬 or 맵 캐릭터 스폰 오브젝트 이름")]
    public string spawnPointId; // 스폰 포인트 이름
    [Header("Cam & Item 초기화 여부")]
    public bool useInitializeCamAndItem;

    public string curmapName;

    public string nextmapName;

}
