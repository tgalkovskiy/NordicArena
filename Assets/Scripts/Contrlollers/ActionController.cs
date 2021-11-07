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
public  class ActionController
{
     public float distanceSkill = 2f;
     public float distanceTake = 1;
     public TypeAttack _TypeAttack = TypeAttack.Combat;
     private GameObject _movePos;
     private NavMeshAgent _agent;
     public GameObject VFX;
     private View _view;
     private AnimationController _animationController;
     private Transform _player;
     private Transform _targetObj;
     private TypePosition _typePosition = TypePosition.DefaultPos;
     Vector3 endPose =Vector3.one;
     public ActionController(NavMeshAgent agent, AnimationController animationController, Transform player, GameObject _VFX, GameObject movePos, View view)
     {
          _agent = agent;
          _animationController = animationController;
          _player = player;
          VFX = _VFX;
          _movePos = movePos;
          _view = view;
     }
     public void GetPosition(Vector3 endPos, TypePosition typePosition, Transform targetObj)
     {
          endPose = endPos;
          _targetObj = targetObj;
          _movePos.SetActive(true);
          _movePos.transform.position = new Vector3(endPos.x, endPos.y+0.1f, endPos.z);
          _typePosition = typePosition;
          switch (typePosition)
          {
               case TypePosition.DefaultPos: _agent.stoppingDistance = 1;  _agent.SetDestination(endPos); break;
               case TypePosition.AttackPos: if (Vector3.Distance(_player.position, endPos) > distanceSkill) 
               {
                    _agent.stoppingDistance = distanceSkill;
                    _agent.SetDestination(endPos);
               }break;
               case TypePosition.TakePos: _agent.stoppingDistance = 1;  _agent.SetDestination(endPos); break;
          }
     }
     public void ActionState()
     {
          _animationController.MoveAnimation(_agent.velocity.magnitude);
          if (_typePosition == TypePosition.AttackPos)
          {
               if (Vector3.Distance(_player.position, endPose) <= distanceSkill + 0.1f && _agent.velocity.magnitude<0.1f)
               {
                    if (_targetObj != null)
                    {
                         Rotation(_targetObj);
                    }
                    _animationController.AttackVariant(_TypeAttack);
                    //VFXManager.Instance.VFXEffect(VFX, 3);
                    _typePosition = TypePosition.DefaultPos;
               } 
          }
          if(_typePosition == TypePosition.TakePos)
          {
               if(Vector3.Distance(_player.position, endPose) <= distanceTake + 0.1f)
               {
                    if (_targetObj != null)
                    {
                         Rotation(_targetObj);
                    }
                    _animationController.TakeItem();
                    _view.SetDataCell(_targetObj.GetComponent<DataObj>()._Data, _targetObj.gameObject);
                    _typePosition = TypePosition.DefaultPos;
               } 
          }
     }
     private void Rotation(Transform targetObj)
     {
          var relativePos = targetObj.position - _player.position;
          _player.rotation = Quaternion.LookRotation(relativePos);
     }

     

}