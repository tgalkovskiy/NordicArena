
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
     public ActionController(View view)
     {
          stateControllers = view.statePerson;
          stats = view.stats;
          pointPatrol = view.pointPatrol;
          targetObj = stateControllers.transform;
     }
     public void GetTarget(RaycastHit target)
     {
          switch(stateControllers.typeAction)
          {
               case TypeAction.Move: stateControllers.Move(1, target.point); break;
               case TypeAction.Patrol when !isPatrol: isPatrol = true;  
                    targetObj = pointPatrol[Random.Range(0, pointPatrol.Length)]; stateControllers.Move(1,targetObj.position);
                    break;
               case TypeAction.AttackToStay: pointTarget = target.point; break;
               case TypeAction.Attack: targetObj = target.collider.transform; stateControllers.MoveWithAttack(targetObj); break;
               case TypeAction.Take: targetObj = target.transform; stateControllers.Move(2,target.collider.transform.position);break;
          }
     }
     private bool ChekDistance()
     {
          return Vector3.Distance(stateControllers.transform.position, targetObj.position) <= stateControllers.view.agent.stoppingDistance;
     }
     private void BaseAction()
     {
          Rotation();
          stateControllers.view.agent.ResetPath();
          stateControllers.view.animationController.AnimationState(stateControllers);
          stateControllers.executeState = ExecuteState.Execute;
     }
     public void ActionState()
     {
          ExecuteAction();
          stateControllers.view.animationController.MoveAnimation(stateControllers.view.agent.velocity.magnitude);
     }
     private void ExecuteAction()
     {
          switch(stateControllers.typeAction)
          {
               case TypeAction.Idle: stateControllers.typeAction = TypeAction.Patrol; isPatrol = false; break;
               case TypeAction.Attack : stateControllers.Attack(targetObj); break;
               case TypeAction.AttackToStay : stateControllers.AttackToStay(pointTarget); break;
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
          stateControllers.view.animationController.AnimationState(stateControllers);
     }
     public void Die()
     {
          stateControllers.stateLife = StateLife.Dead;
          stateControllers.view.animationController.AnimationState(stateControllers);
     }
}