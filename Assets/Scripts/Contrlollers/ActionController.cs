
using UnityEngine;
using DG.Tweening;
public sealed class ActionController
{
     private Stats stats;
     private StateControllers stateControllers;
     private Transform[] pointPatrol;
     private Transform _targetObj;
     private float _delay = 0;
     
     public ActionController(StateControllers stateControllers)
     {
          this.stateControllers = stateControllers;
          stats = this.stateControllers.view._stats;
          pointPatrol = this.stateControllers.pointPatrol;
     }
     public void GetPosition(Vector3 endPos, Transform targetObj)
     {
          _targetObj = targetObj;
          switch(stateControllers.state)
          {
               case State.Move: stateControllers.agent.stoppingDistance = 1;  stateControllers.agent.SetDestination(endPos); break;
               case State.Patrol: stateControllers.agent.SetDestination(endPos); break;
               case State.Attack: if (Vector3.Distance(stateControllers.transform.position, endPos) > stats.distanceSkill) 
               {
                    stateControllers.agent.stoppingDistance = stats.distanceSkill;
                    stateControllers.agent.SetDestination(endPos);
               }break;
               case State.Take: stateControllers.agent.stoppingDistance = 0.5f;  stateControllers.agent.SetDestination(endPos); break;
          }
     }
     public void ActionState()
     {
          if (stateControllers.state == State.Die) return;
          ExecuteAction();
          stateControllers._animationController.MoveAnimation(stateControllers.agent.velocity.magnitude);
     }
    
     private void ExecuteAction()
     {
          if(stateControllers.agent.remainingDistance <= stateControllers.agent.stoppingDistance && stateControllers.agent.velocity.magnitude < 0.1)
          {
               switch(stateControllers.state)
               {
                    case State.Attack when stateControllers.executeState != ExecuteState.Execute : Attack(); break;
                    case State.Take when stateControllers.executeState != ExecuteState.Execute: Take(); break;
                    case State.Patrol when _delay<=0: Patrol(); break;
                    case State.Menu: break;
                    case State.OnOfWeapon when stateControllers.executeState != ExecuteState.Execute: OnOfWeapon(); break;
               }
          }
          else if(stateControllers.agent.remainingDistance >= 20)
          {
               stateControllers.state = State.Patrol;
          }
     }
     private void Patrol()
     {
          if(pointPatrol.Length <= 0) return;
          _delay = stats.delayPatrol;
          DOTween.To(() => _delay, x => _delay = x, 0, stats.delayPatrol).OnComplete(()=>
          {
               stateControllers._animationController.AnimationState(stateControllers);
               GetPosition(pointPatrol[Random.Range(0, pointPatrol.Length)].position, null);
          }).Kill();
     }

     private void Take()
     {
          Rotation(_targetObj.position);
          stateControllers._animationController.AnimationState(stateControllers);
          stateControllers.transform.GetComponent<PlayerView>()
               .SetDataCell(_targetObj.GetComponent<DataObj>()._Data, _targetObj.gameObject);
          stateControllers.executeState = ExecuteState.Execute;
     }

     private void Attack()
     {
          Rotation(_targetObj.position);
          stateControllers._animationController.AnimationState(stateControllers);
          stateControllers.damageDiller.SetDamage(stats);
          stateControllers.executeState = ExecuteState.Execute;
     }
     
     public void Rotation(Vector3 target)
     {
          var relativePos = target - stateControllers.transform.position;
          stateControllers.transform.rotation = Quaternion.LookRotation(relativePos);
     }

     private void OnOfWeapon()
     {
          stateControllers._animationController.AnimationState(stateControllers);
     }
     public void Die()
     {
          stateControllers.state = State.Die;
          stateControllers._animationController.AnimationState(stateControllers);
     }
}