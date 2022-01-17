
using UnityEngine;


public class StateControllers : MonoBehaviour
{
    public Race race;
    public RelationShip relationShip;
    public StateLife stateLife;
    public TypeAction typeAction;
    public ExecuteState executeState;
    public View view;
    public ActionController actionController;
    
    public void InitActionController(View view)
    {
        this.view = view;
        actionController = new ActionController(view);
    }

    public void Move(float minStopDistance, Vector3 position)
    {
        view.agent.stoppingDistance = minStopDistance;
        view.agent.SetDestination(position);
    }

    public void MoveWithAttack(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > view.stats.distanceSkill)
        {
            Move(view.stats.distanceSkill, target.position);
        }
    }

    public void Attack(Transform target)
    {
        if(executeState == ExecuteState.Execute) return;
        if(!ChekDistance(target)) return;
        Rotation(target.position);
        view.agent.ResetPath();
        view.damageDiller.SetDamage(view.stats);
        view.animationController.AnimationState(view.statePerson);
        view.statePerson.executeState = ExecuteState.Execute;
    }

    public void AttackToStay(Vector3 target)
    {
        if(executeState == ExecuteState.Execute) return;
        Rotation(target);
        view.agent.ResetPath();
        view.damageDiller.SetDamage(view.stats);
        view.animationController.AnimationState(view.statePerson);
        view.statePerson.executeState = ExecuteState.Execute;
    }
    
    private bool ChekDistance(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) <= view.agent.stoppingDistance;
    }
    private void Rotation(Vector3 target)
    {
        Vector3 relativePos = target - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos); 
    }
   




}
