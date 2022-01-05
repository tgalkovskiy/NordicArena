
using UnityEngine;
using DG.Tweening;
public sealed class ActionController
{
     private Stats stats;
     private StateControllers stateControllers;
     private Transform[] pointPatrol;
     private Transform _targetObj;
     private Vector3 pointTarget;
     private float _delay = 0;
     
     public ActionController(StateControllers stateControllers)
     {
          this.stateControllers = stateControllers;
          stats = this.stateControllers.view._stats;
          pointPatrol = this.stateControllers.pointPatrol;
     }
     public void GetPositionMove(Vector3 endPos, Transform targetObj)
     {
          _targetObj = targetObj.transform;
          pointTarget = targetObj.position;
          switch(stateControllers.state)
          {
               case State.Move: stateControllers.agent.stoppingDistance = 1;  stateControllers.agent.SetDestination(endPos); break;
               case State.Patrol: stateControllers.agent.SetDestination(endPos); break;
               case State.Attack: if (Vector3.Distance(stateControllers.transform.position, endPos) > stats.distanceSkill) 
               {
                    stateControllers.agent.stoppingDistance = stats.distanceSkill;
                    stateControllers.agent.SetDestination(endPos);
               }break;
               case State.Take: stateControllers.agent.stoppingDistance = 1f;  stateControllers.agent.SetDestination(endPos); break;
          }
     }
     public void GetTarget(RaycastHit targetObj)
     { 
          _targetObj = targetObj.transform;
          pointTarget = targetObj.point;
     }

     private bool ChekDistance()
     {
          //Vector3.Distance(stateControllers.transform.position, pointTarget)
          return stateControllers.agent.remainingDistance <= stateControllers.agent.stoppingDistance &&
                 stateControllers.agent.velocity.magnitude < 0.1;
     }
     private void BaseAction()
     {
          Rotation();
          //stateControllers.agent.ResetPath();
          stateControllers._animationController.AnimationState(stateControllers);
          stateControllers.executeState = ExecuteState.Execute;
     }
     public void ActionState()
     {
          if (stateControllers.state == State.Die) return;
          ExecuteAction();
          stateControllers._animationController.MoveAnimation(stateControllers.agent.velocity.magnitude);
     }
     
     
     private void ExecuteAction()
     {
          switch(stateControllers.state)
          {
               case State.Attack when stateControllers.executeState != ExecuteState.Execute && ChekDistance(): Attack(); break;
               case State.Take when stateControllers.executeState != ExecuteState.Execute && ChekDistance(): Take(); break;
               case State.Patrol when _delay<=0 && ChekDistance(): Patrol(); break;
               case State.Menu: break;
               case State.OnOfWeapon when stateControllers.executeState != ExecuteState.Execute: OnOfWeapon(); break;
          }
          if(stateControllers.agent.remainingDistance >= 20)
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
               GetPositionMove(pointPatrol[Random.Range(0, pointPatrol.Length)].position, null);
          }).Kill();
     }

     private void Take()
     {
          //Rotation();
          //stateControllers._animationController.AnimationState(stateControllers);
          Debug.Log(1);
          BaseAction();
          stateControllers.transform.GetComponent<PlayerView>()
               .SetDataCell(_targetObj.GetComponent<DataObj>()._Data, _targetObj.gameObject);
          //stateControllers.executeState = ExecuteState.Execute;
     }

     private void Attack()
     {
          //Rotation();
          //stateControllers._animationController.AnimationState(stateControllers);
          BaseAction();
          stateControllers.damageDiller.SetDamage(stats);
          //stateControllers.executeState = ExecuteState.Execute;
     }
     
     private void Rotation()
     {
          var relativePos = pointTarget - stateControllers.transform.position;
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