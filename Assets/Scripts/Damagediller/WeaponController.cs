using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public static class WeaponController
{
     public static float distance = 5f;
     
     

     public static bool Attack(Vector3 currentPosition, Vector3 endPosition, float agentDistance, bool isAttack)
     {
          if (Vector3.Distance(currentPosition, endPosition) <= agentDistance + distance && isAttack)
          {
               AnimationController.Instance.AttackVariant(0);
               return false;
          }
          return isAttack;
     }

}