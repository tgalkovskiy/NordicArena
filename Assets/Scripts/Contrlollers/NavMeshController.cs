using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public enum TypePosition
{
     DefaultPos,
     TakePos,
     AttackPos
}
public  class NavMeshController
{
     public float distance = 5f;

     private NavMeshAgent _agent;
     private AnimationController _animationController;
     private Transform _player;
     private bool isAttack;
     Vector3 endPose =Vector3.one;
     public NavMeshController(NavMeshAgent agent, AnimationController animationController, Transform player)
     {
          _agent = agent;
          _animationController = animationController;
          _player = player;
     }
     public void GetPosition(Vector3 endPos, TypePosition typePosition)
     {
          endPose = endPos;
          switch (typePosition)
          {
               case TypePosition.DefaultPos: _agent.stoppingDistance = 1;  _agent.SetDestination(endPos); isAttack = false; break;
               case TypePosition.AttackPos:
                    if (Vector3.Distance(_player.position, endPos) > distance)
                    {
                         _agent.stoppingDistance = distance;
                         _agent.SetDestination(endPos);
                         isAttack = true;
                    }
                    else
                    {
                         isAttack = true;  
                    }
                    break;
          }
     }
     public  bool Attack(Vector3 currentPosition, Vector3 endPosition, float agentDistance, bool isAttack, AnimationController animationController)
     {
          if (Vector3.Distance(currentPosition, endPosition) <= agentDistance + distance && isAttack)
          {
               _animationController.AttackVariant(0);
               return false;
          }
          return isAttack;
     }
     public void NawMeshState()
     {
          _animationController.MoveAnimation(_agent.velocity.magnitude);
          if (Vector3.Distance(_player.position, endPose) <= distance && isAttack)
          {
               _animationController.AttackVariant(0);
               isAttack = !isAttack;
          }
     }

}