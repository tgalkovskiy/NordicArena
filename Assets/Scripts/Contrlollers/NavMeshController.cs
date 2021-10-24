using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public  class NavMeshController
{
     public float distance = 8f;

     private NavMeshAgent _agent;
     private AnimationController _animationController;
     private Transform _player;
     
     public NavMeshController(NavMeshAgent agent, AnimationController animationController, Transform player)
     {
          _agent = agent;
          _animationController = animationController;
          _player = player;
     }
     public void Move(Vector3 endPos)
     {
          _agent.SetDestination(endPos);
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
     }

}