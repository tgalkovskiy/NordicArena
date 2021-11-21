using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Stats", fileName = "Stats")]
public class Stats : ScriptableObject
{
   public float _hpNow;
   public float _hpMax;
   public float _manaNow;
   public float _manaMax;
   public float _armor;
   public float _damage;
   public float _delayDamage;
   public float _coolDown;
   public float _distanceSkill;
   public float _delayPatrol;
   public TypeAttack _TypeAttack;
}
