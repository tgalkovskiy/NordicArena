using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public  class WeaponController
{
     public float distance = 8f;
     public  bool Attack(Vector3 currentPosition, Vector3 endPosition, float agentDistance, bool isAttack, AnimationController animationController)
     {
          if (Vector3.Distance(currentPosition, endPosition) <= agentDistance + distance && isAttack)
          {
               animationController.AttackVariant(0);
               return false;
          }
          return isAttack;
     }

}