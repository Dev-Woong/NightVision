using UnityEngine;

[CreateAssetMenu(fileName = "BGMData", menuName = "BGM/BGMData")]
public class BGMData : ScriptableObject
{
    public AudioClip BGM;
    public int sceneNum;
}
