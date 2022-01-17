
using System;
using UnityEngine;

public class BehaviourPerson : MonoBehaviour
{
    private MonstersView monstersView;

    private void Start()
    {
        monstersView = GetComponent<MonstersView>();
    }

    private void Update()
    {
        if(monstersView.statePerson.stateLife == StateLife.Dead) return;
        monstersView.hits = Physics.SphereCastAll(transform.position, 10, Vector3.forward, 10);
        foreach (var I in monstersView.hits)
        {
            if(ChekEnemy(I))
            {
                monstersView.statePerson.typeAction = TypeAction.Attack;
            }
            monstersView.statePerson.actionController.GetTarget(I);
        }
        monstersView.statePerson.actionController.ActionState();
    }

    private bool ChekEnemy(RaycastHit hit)
    {
        if(hit.collider.gameObject.GetComponent<PlayerView>())
        {
            if(hit.collider.gameObject.GetComponent<PlayerView>().statePerson.stateLife == StateLife.Dead) return false;
            else if(hit.collider.gameObject.GetComponent<PlayerView>().statePerson.relationShip ==  monstersView.statePerson.relationShip) return false;
            else { return true;}
        }
        else
        {
            return false;
        }
    }
}
