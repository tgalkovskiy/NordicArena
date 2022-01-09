
using UnityEngine;

public class BehaviourPerson : StateControllers
{
    private void Update()
    {
        if(stateLife == StateLife.Dead) return;
        hits = Physics.SphereCastAll(transform.position, 10, Vector3.forward, 10);
        foreach (var I in hits)
        {
            if(ChekEnemy(I))
            {
                typeAction = TypeAction.Attack;
            }
            actionController.GetTarget(I);
        }
        actionController.ActionState();
    }

    private bool ChekEnemy(RaycastHit hit)
    {
        if(hit.collider.gameObject.GetComponent<InputController>())
        {
            if(hit.collider.gameObject.GetComponent<InputController>().stateLife == StateLife.Dead) return false;
            else if(hit.collider.gameObject.GetComponent<InputController>().relationShip == relationShip) return false;
            else { return true;}
        }
        else
        {
            return false;
        }
    }
}
