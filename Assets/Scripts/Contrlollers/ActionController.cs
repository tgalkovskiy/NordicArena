
using System.Threading.Tasks;
using UnityEngine;
public sealed class ActionController 
{
     private Stats stats;
     private StateControllers stateControllers;
     private Transform[] pointPatrol;
     private Transform targetObj;
     private Vector3 pointTarget;
     private bool isPatrol = false;
     public ActionController(StateControllers stateControllers)
     {
          this.stateControllers = stateControllers;
          stats = this.stateControllers.view.stats;
          pointPatrol = this.stateControllers.pointPatrol;
          targetObj = stateControllers.transform;
     }
     public void GetTarget(RaycastHit target)
     {
          switch(stateControllers.typeAction)
          {
               case TypeAction.Move: stateControllers.agent.stoppingDistance = 1; stateControllers.agent.SetDestination(target.point); break;
               case TypeAction.Patrol when !isPatrol: isPatrol = true; stateControllers.agent.stoppingDistance = 1; 
                    targetObj = pointPatrol[Random.Range(0, pointPatrol.Length)];
                    stateControllers.agent.SetDestination(targetObj.position);
                    break;
               case TypeAction.AttackToStay: pointTarget = target.point; break;
               case TypeAction.Attack: targetObj = target.collider.transform;
                    if (Vector3.Distance(stateControllers.transform.position, target.collider.transform.position) > stats.distanceSkill)
                    {
                         stateControllers.agent.stoppingDistance = stats.distanceSkill;
                         stateControllers.agent.SetDestination(target.collider.transform.position);
                    }
                    break;
               case TypeAction.Take: targetObj = target.transform; stateControllers.agent.stoppingDistance = 2f; 
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
          ExecuteAction();
          stateControllers._animationController.MoveAnimation(stateControllers.agent.velocity.magnitude);
     }
     private void ExecuteAction()
     {
          switch(stateControllers.typeAction)
          {
               case TypeAction.Idle: stateControllers.typeAction = TypeAction.Patrol; isPatrol = false; break;
               case TypeAction.Attack when stateControllers.executeState != ExecuteState.Execute && ChekDistance(): Attack(); break;
               case TypeAction.AttackToStay when stateControllers.executeState != ExecuteState.Execute: Attack(); break;
               case TypeAction.Take when stateControllers.executeState != ExecuteState.Execute && ChekDistance(): Take(); break;
               case TypeAction.Patrol when stateControllers.executeState != ExecuteState.Execute && ChekDistance(): Patrol(); break;
               case TypeAction.Menu: break;
               case TypeAction.OnOfWeapon when stateControllers.executeState != ExecuteState.Execute: OnOfWeapon(); break;
          }
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
          Vector3 relativePos;
          if(stateControllers.typeAction == TypeAction.AttackToStay)
          {
              relativePos = pointTarget - stateControllers.transform.position;
              stateControllers.transform.rotation = Quaternion.LookRotation(relativePos); 
          }
          else
          {
              relativePos = targetObj.position - stateControllers.transform.position;
               stateControllers.transform.rotation = Quaternion.LookRotation(relativePos); 
          }
     }
     private void OnOfWeapon()
     {
          stateControllers._animationController.AnimationState(stateControllers);
     }
     public void Die()
     {
          stateControllers.stateLife = StateLife.Dead;
          stateControllers._animationController.AnimationState(stateControllers);
     }
}