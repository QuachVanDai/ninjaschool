using UnityEngine;

[CreateAssetMenu(menuName = "Animations/Frame Skill")]
public class FrameSkill : ScriptableObject
{
    public IDSkill IDSkill;
    public Sprite icon;
    public string skillName;
    public int requiresLevel;
    public int skillLevel;
    public Sprite[] framesMove;
    public Sprite[] framesFont;
    public Sprite[] framesStart;
    public Sprite[] framesPosMonster;
    public int mp;
    public float timeSkill;
    public int skillDamage;
    public int increasedHPMP;
    public float coefficient;
    public bool isBlock=false;
    public bool isActack=false;
    public string description;
}
public enum IDSkill
{
    none =0,
    SkillLv1 =1,
    SkillLv5 =2,
    SkillLv10 =3,
    SkillLv15 =4,
    SkillLv20 =5,
}