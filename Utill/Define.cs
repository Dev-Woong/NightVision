using UnityEngine;

public class Define 
{
    public const string Horizontal = "Horizontal";
    public readonly static int isWalkHash = Animator.StringToHash("isWalk");
    public readonly static int isRunHash = Animator.StringToHash("isRun");
    public readonly static int DashHash = Animator.StringToHash("Dash");
    public readonly static int useSkillHash = Animator.StringToHash("Skill");
    public readonly static int comboCountHash = Animator.StringToHash("ComboStep");
    public readonly static int HurtHash = Animator.StringToHash("Hurt");
}
