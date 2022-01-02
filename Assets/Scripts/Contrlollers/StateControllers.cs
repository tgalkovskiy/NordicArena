
using UnityEngine;
using UnityEngine.AI;

public class StateControllers : MonoBehaviour
{
    public AnimationController _animationController;
    public DamageDiller damageDiller;
    public State state;
    public ExecuteState executeState;
    public Transform[] pointPatrol;
    public View view;
    public NavMeshAgent agent;
    public RaycastHit[] hits = default;
    public ActionController actionController;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        damageDiller = GetComponent<DamageDiller>();
        view = GetComponent<View>();
    }
    private void Start()
    {
        actionController = new ActionController(this);
    }




}
