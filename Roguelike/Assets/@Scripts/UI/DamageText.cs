using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;

    public TMP_Text damageText;
    Color alpha;

    public int damage;

    void Start()
    {
        damageText = GetComponent<TMP_Text>();
        damageText.text = damage.ToString();
        alpha = damageText.color;
        StartCoroutine(UnDamageText());
    }

    
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        damageText.color = alpha;
    }

    IEnumerator UnDamageText()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
