
using UnityEngine;
using DG.Tweening;
public sealed class ActionController
{
     private Stats _stats;
     private StateControllers _stateControllers;
     private Transform[] _pointPatrol;
     private Transform _targetObj;
     private float _cooldown =0 ;
     private float _delay = 0;
     
     public ActionController(StateControllers stateControllers)
     {
          _stateControllers = stateControllers;
          _stats = _stateControllers._view._stats;
          _pointPatrol = _stateControllers.pointPatrol;
     }
     public void GetPosition(Vector3 endPos, Transform targetObj)
     {
          _targetObj = targetObj;
          switch(_stateControllers._state)
          {
               case State.Move: _stateControllers._agent.stoppingDistance = 1;  _stateControllers._agent.SetDestination(endPos); break;
               case State.Patrol: _stateControllers._agent.SetDestination(endPos); break;
               case State.Attack: if (Vector3.Distance(_stateControllers.transform.position, endPos) > _stats._distanceSkill) 
               {
                    _stateControllers._agent.stoppingDistance = _stats._distanceSkill;
                    _stateControllers._agent.SetDestination(endPos);
               }break;
               case State.Take: _stateControllers._agent.stoppingDistance = 0.5f;  _stateControllers._agent.SetDestination(endPos); break;
          }
     }
     public void ActionState()
     {
          if (_stateControllers._state == State.Die) return;
          _stateControllers._animationController.MoveAnimation(_stateControllers._agent.velocity.magnitude);
          ExecuteAction();
     }
    
     private void ExecuteAction()
     {
          if(_stateControllers._agent.remainingDistance <= _stateControllers._agent.stoppingDistance && _stateControllers._agent.velocity.magnitude < 0.1)
          {
               switch (_stateControllers._state)
               {
                    case State.Attack when _cooldown<=0: Attack(); break;
                    case State.Take: Take(); break;
                    case State.Patrol when _delay<=0: Patrol(); break;
               }
          }
          else if(_stateControllers._agent.remainingDistance >= 20)
          {
               _stateControllers._state = State.Patrol;
          }
     }
     private void Patrol()
     {
          if(_pointPatrol.Length <= 0) return;
          _delay = _stats._delayPatrol;
          DOTween.To(() => _delay, x => _delay = x, 0, _stats._delayPatrol).OnComplete(()=>
          {
               _stateControllers._animationController.AnimationState(_stateControllers);
               GetPosition(_pointPatrol[Random.Range(0, _pointPatrol.Length)].position, null);
          });
     }

     private void Take()
     {
          Rotation();
          _stateControllers._animationController.AnimationState(_stateControllers);
          _stateControllers.transform.GetComponent<PlayerView>()
               .SetDataCell(_targetObj.GetComponent<DataObj>()._Data, _targetObj.gameObject);
     }

     private void Attack()
     {
          Rotation();
          _cooldown = _stats._coolDown;
          DOTween.To(() => _cooldown, x => _cooldown = x, 0, _stats._coolDown).OnComplete(()=>
          {
               _stateControllers._animationController.AnimationState(_stateControllers);
               _stateControllers.damageDiller.Damage(_stats);
          });
     }
     
     private void Rotation()
     {
          if (_targetObj == null) return;
          var relativePos = _targetObj.position - _stateControllers.transform.position;
          _stateControllers.transform.rotation = Quaternion.LookRotation(relativePos);
     }

     public void Die()
     {
          _stateControllers._state = State.Die;
          _stateControllers._animationController.AnimationState(_stateControllers);
     }
}