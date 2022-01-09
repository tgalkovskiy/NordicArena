
using UnityEngine;
using UnityEngine.AI;

public class StateControllers : MonoBehaviour
{
    public Race race;
    public RelationShip relationShip;
    public StateLife stateLife;
    public TypeAction typeAction;
    public ExecuteState executeState;
    [HideInInspector] public AnimationController _animationController;
    [HideInInspector] public DamageDiller damageDiller;
    [HideInInspector] public Transform[] pointPatrol;
    [HideInInspector] public View view;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public VFXManager vfxManager;
    public RaycastHit[] hits = default;
    public ActionController actionController;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        damageDiller = GetComponent<DamageDiller>();
        view = GetComponent<View>();
        vfxManager = GetComponent<VFXManager>();
    }
    private void Start()
    {
        actionController = new ActionController(this);
    }




}
