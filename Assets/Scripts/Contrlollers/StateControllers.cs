
using UnityEngine;
using UnityEngine.AI;

public class StateControllers : MonoBehaviour
{
    public AnimationController _animationController;
    public DamageDiller _damageDiller;
    public State _state;
    public Transform[] pointPatrol;
    public View _view;
    public NavMeshAgent _agent;
    public RaycastHit[] _hits = default;
    public ActionController _actionController;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _damageDiller = GetComponent<DamageDiller>();
        _view = GetComponent<View>();
    }
    private void Start()
    {
        _actionController = new ActionController(this);
    }




}
