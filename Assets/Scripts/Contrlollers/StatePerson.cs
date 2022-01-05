
using UnityEngine;

public class StatePerson : StateControllers
{
    private void FixedUpdate()
    {
        if(state == State.Die) return;
        hits = Physics.SphereCastAll(transform.position, 10, Vector3.forward, 10);
        foreach (var I in hits)
        {
            if(I.collider.gameObject.GetComponent<InputController>() && I.collider.gameObject.GetComponent<InputController>().state != State.Die)
            {
                if(state != State.Idle && state != State.Patrol)continue;
                state = State.Attack;
                actionController.GetPositionMove(I.collider.transform.position, I.transform);
            }
            else
            {
                state = State.Patrol;
            }
        }
        actionController.ActionState();
    }
}
