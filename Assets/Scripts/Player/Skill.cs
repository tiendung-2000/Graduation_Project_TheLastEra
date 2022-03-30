using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/NewSkill")]

public class Skill : ScriptableObject
{
    public Sprite icon;
    public float skillDamage;
    public float cooldownSkill;
}
