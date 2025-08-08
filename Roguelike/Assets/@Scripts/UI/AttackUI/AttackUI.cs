using UnityEngine;
using UnityEngine.UI;

public class AttackUI : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    public Image NormalAtkImage;
    public Image Skill_1_Image;
    public Image Skill_2_Image;
    public Sprite[] NormalAtkSpr;
    public Sprite[] Skill_1_Spr;
    public Sprite[] Skill_2_Spr;
    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        NormalAtkImage.sprite = NormalAtkSpr[0];
        Skill_1_Image.sprite = Skill_1_Spr[0];
        Skill_2_Image.sprite = Skill_2_Spr[0];
    }
    public void ChangeSkillSpriteUI()
    {
        if (pc.weaponType == WeaponType.Hand)
        {
            NormalAtkImage.sprite = NormalAtkSpr[0];
            Skill_1_Image.sprite = Skill_1_Spr[0];
            Skill_2_Image.sprite = Skill_2_Spr[0];
        }
        if (pc.weaponType == WeaponType.Sword)
        {
            NormalAtkImage.sprite = NormalAtkSpr[1];
            Skill_1_Image.sprite = Skill_1_Spr[1];
            Skill_2_Image.sprite = Skill_2_Spr[1];
        }
        if (pc.weaponType == WeaponType.Gun)
        {
            NormalAtkImage.sprite = NormalAtkSpr[2];
            Skill_1_Image.sprite = Skill_1_Spr[2];
            Skill_2_Image.sprite = Skill_2_Spr[2];
        }
    }
    void Update()
    {
        if (LoadingController.onInputBlocker == false && LoadingController.onOpenShop == false && LoadingController.onPause == false)
        {
            ChangeSkillSpriteUI();
        }
    }
}
