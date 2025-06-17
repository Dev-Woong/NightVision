using UnityEngine;

public class Effect_Connect : MonoBehaviour
{
    /// <summary>
    /// AnimationEvent
    /// </summary>
    public GameObject ConnectEffect;
    public Transform EffectTransform;

    public void InstantiateEffect()
    {
        GameObject effect = Instantiate(ConnectEffect);
        effect.transform.position = EffectTransform.position;

    }
}
