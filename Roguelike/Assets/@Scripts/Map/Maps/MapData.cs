using UnityEngine;

[CreateAssetMenu(menuName = "MapData")]
public class MapData : ScriptableObject
{
    [Header("���� �� ��")]
    public string sceneName; // �� ��
    [Header("���� �� or �� ĳ���� ���� ������Ʈ �̸�")]
    public string spawnPointId; // ���� ����Ʈ �̸�
    [Header("Cam & Item �ʱ�ȭ ����")]
    public bool useInitializeCamAndItem;

    public string curmapName;

    public string nextmapName;

}
