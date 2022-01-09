
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats", fileName = "Stats")]
public class Stats : ScriptableObject
{
   public float hpNow;
   public float hpMax;
   public float manaNow;
   public float manaMax;
   public float armor;
   public float damage;
   public float delayDamage;
   public float coolDown;
   public float distanceSkill;
   public float delayPatrol;
   public TypeAttack TypeAttack;
   public List<Race> EnemyRace;
}
