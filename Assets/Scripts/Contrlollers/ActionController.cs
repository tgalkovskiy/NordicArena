
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
public sealed class ActionController
{
     private Stats stats;
     private StateControllers stateControllers;
     private Transform[] pointPatrol;
     private Transform targetObj;
     private Vector3 pointTarget;
     private float _delay = 0;
     private bool isPatrol = false;
     public ActionController(StateControllers stateControllers)
     {
          this.stateControllers = stateControllers;
          stats = this.stateControllers.view._stats;
          pointPatrol = this.stateControllers.pointPatrol;
          targetObj = stateControllers.transform;
     }
     public void GetTarget(RaycastHit target)
     {
          switch(stateControllers.state)
          {
               case State.Move:
                    stateControllers.agent.stoppingDistance = 1; 
                    stateControllers.agent.SetDestination(target.point); break;
               case State.Patrol when !isPatrol:
                    isPatrol = true;
                    stateControllers.agent.stoppingDistance = 1;
                    targetObj = pointPatrol[Random.Range(0, pointPatrol.Length)];
                    stateControllers.agent.SetDestination(targetObj.position);
                    break;
               case State.AttackToStay:
                    pointTarget = target.point;
                    break;
               case State.Attack:
                    this.targetObj = target.collider.transform;
                    if (Vector3.Distance(stateControllers.transform.position, target.collider.transform.position) > stats.distanceSkill)
                    {
                         stateControllers.agent.stoppingDistance = stats.distanceSkill;
                         stateControllers.agent.SetDestination(target.collider.transform.position);
                    }
                    break;
               case State.Take: 
                    this.targetObj = target.transform;
                    stateControllers.agent.stoppingDistance = 2f;  
                    stateControllers.agent.SetDestination(target.collider.transform.position); break;
          }
     }
     private bool ChekDistance()
     {
          return Vector3.Distance(stateControllers.transform.position, targetObj.position) <= stateControllers.agent.stoppingDistance;
     }
     private void BaseAction()
     {
          Rotation();
          stateControllers.agent.ResetPath();
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
               case State.Idle: stateControllers.state = State.Patrol; break;
               case State.Attack when stateControllers.executeState != ExecuteState.Execute && ChekDistance(): Attack(); break;
               case State.AttackToStay when stateControllers.executeState != ExecuteState.Execute: Attack(); break;
               case State.Take when stateControllers.executeState != ExecuteState.Execute && ChekDistance(): Take(); break;
               case State.Patrol when stateControllers.executeState != ExecuteState.Execute && ChekDistance(): Patrol(); break;
               case State.Menu: break;
               case State.OnOfWeapon when stateControllers.executeState != ExecuteState.Execute: OnOfWeapon(); break;
          }
          /*if(Vector3.Distance(stateControllers.transform.position, targetObj.position)>=20)
          {
               Debug.Log("patrol");
               stateControllers.state = State.Patrol;
               isPatrol = false;
          }*/
     }
     private async void Patrol()
     {
          if(pointPatrol.Length <= 0) return;
          stateControllers.executeState = ExecuteState.Execute;
          await Task.Delay((int) stats.delayPatrol * 1000);
          isPatrol = false;
          stateControllers.executeState = ExecuteState.NonExecute;
     }

     private void Take()
     {
          BaseAction();
          stateControllers.transform.GetComponent<PlayerView>()
               .SetDataCell(targetObj.GetComponent<DataObj>()._Data, targetObj.gameObject);
     }

     private void Attack()
     {
          BaseAction();
          stateControllers.damageDiller.SetDamage(stats);
     }
     
     private void Rotation()
     {
          if(stateControllers.state == State.AttackToStay)
          {
              var relativePos = pointTarget - stateControllers.transform.position;
              stateControllers.transform.rotation = Quaternion.LookRotation(relativePos); 
          }
          else
          {
               var relativePos = targetObj.position - stateControllers.transform.position;
               stateControllers.transform.rotation = Quaternion.LookRotation(relativePos); 
          }
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