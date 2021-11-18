using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePerson : StateControllers
{
    private void Update()
    {
        _hits = Physics.SphereCastAll(transform.position, 10, Vector3.forward, 10);
        foreach (var I in _hits)
        {
            if(I.collider.gameObject.GetComponent<InputController>())
            {
                if(_state == State.Stay || _state == State.Patrol)
                {
                    _state = State.Attack;
                    _actionController.GetPosition(I.collider.gameObject.transform.position, I.collider.transform);
                }
            }
            else
            {
                _state = State.Patrol;
            }
        }
        _actionController.ActionState();
    }
}
