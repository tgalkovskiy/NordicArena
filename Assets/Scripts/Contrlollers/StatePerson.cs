
using UnityEngine;

public class StatePerson : StateControllers
{
    
    private void Update()
    {
        if(state == State.Die) return;
        hits = Physics.SphereCastAll(transform.position, 7, Vector3.forward, 7);
        foreach (var I in hits)
        {
            if(I.collider.gameObject.GetComponent<InputController>() && I.collider.gameObject.GetComponent<InputController>().state != State.Die)
            {
                state = State.Attack;
            }
            actionController.GetTarget(I);
        }
        actionController.ActionState();
    }
}
