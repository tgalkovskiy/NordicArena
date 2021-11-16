
using UnityEngine;
using UnityEngine.AI;

public  class ActionController
{
     public float distanceSkill = 4f;
     private float distanceTake = 1;
     public TypeAttack _TypeAttack = TypeAttack.Combat;
     private NavMeshAgent _agent;
     private StateControllers _state;
     private PlayerView _playerView;
     private AnimationController _animationController;
     private Transform _player;
     private Transform _targetObj;
     private Vector3 _endPose =Vector3.one;
     public float _cooldown =3f;
     public ActionController(NavMeshAgent agent, StateControllers state, AnimationController animationController, Transform player, PlayerView playerView)
     {
          _agent = agent;
          _animationController = animationController;
          _player = player;
          _playerView = playerView;
          _state = state;
     }
     public void GetPosition(Vector3 endPos, Transform targetObj)
     {
          _endPose = endPos;
          _targetObj = targetObj;
          switch (_state._state)
          {
               case State.Move: _agent.stoppingDistance = 1;  _agent.SetDestination(endPos); break;
               case State.Patrol: _agent.SetDestination(endPos); break;
               case State.Attack: if (Vector3.Distance(_player.position, endPos) > distanceSkill) 
               {
                    _agent.stoppingDistance = distanceSkill;
                    _agent.SetDestination(endPos);
               }break;
               case State.Take: _agent.stoppingDistance = 1;  _agent.SetDestination(endPos); break;
          }
     }
     public void ActionState()
     {
          _animationController.MoveAnimation(_agent.velocity.magnitude);
          _cooldown -= Time.deltaTime;
          if (_state._state == State.Attack)
          {
               if (_agent.remainingDistance<= _agent.stoppingDistance && _agent.velocity.magnitude<0.1)
               {
                    if (_targetObj != null)
                    {
                         Rotation(_targetObj);
                    }
                    if (_cooldown <= 0)
                    {
                        _animationController.AttackVariant(_TypeAttack);
                        _state._state = State.Stay;
                        _cooldown = 5f;
                    }
               }
               else if (_agent.remainingDistance >= 20)
               {
                    _state._state = State.Patrol;
               }
          }
          if(_state._state == State.Take)
          {
               if(Vector3.Distance(_player.position, _endPose) <= distanceTake + 0.1f)
               {
                    if (_targetObj != null)
                    {
                         Rotation(_targetObj);
                    }
                    _animationController.TakeItem();
                    _playerView.SetDataCell(_targetObj.GetComponent<DataObj>()._Data, _targetObj.gameObject);
                    _state._state = State.Stay;
               } 
          }
          
     }
     private void Rotation(Transform targetObj)
     {
          var relativePos = targetObj.position - _player.position;
          _player.rotation = Quaternion.LookRotation(relativePos);
     }

     

}