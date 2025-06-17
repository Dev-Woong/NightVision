using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamageText : PoolAble
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destroySpeed = 1;
    public float maxFontSize = 15;
    public float currentAlpha = 1;
    public float fontMinusSpeed = 10;
    TMP_Text damageText;
    Color alpha;
    VertexGradient criticalColor;
    WaitForSeconds DelTime;
    public float damage;
    void Start()
    {
        criticalColor = new (Color.white, Color.white, Color.red, Color.red);
        damageText = GetComponent<TMP_Text>();
        if (damage <= 0)
        { damageText.text = "Miss"; }
        else if (damage >= 99)
        {
            damageText.GetComponent<TMP_Text>().colorGradient = criticalColor;
            damageText.text = "99";
        }
        else { damageText.text = damage.ToString(); }
        
        DelTime = new(destroySpeed);
        alpha = damageText.color;
        StartCoroutine(UnDamageText());
    }
    public void TextMove()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        // 알파 변화
        currentAlpha = Mathf.Lerp(currentAlpha, 0, Time.deltaTime * alphaSpeed);
        alpha = damageText.color;
        alpha.a = currentAlpha;
        damageText.color = alpha;

        // 폰트 크기 변화

        damageText.fontSize = Mathf.RoundToInt(maxFontSize -= Time.deltaTime*fontMinusSpeed); // 꼭 정수로
    }
    void Update()
    {
        TextMove();
    }
    IEnumerator UnDamageText()
    {
        yield return DelTime;
        Destroy(gameObject);
    }
}
