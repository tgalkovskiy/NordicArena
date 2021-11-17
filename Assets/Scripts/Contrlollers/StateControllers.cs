using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class StateControllers : MonoBehaviour
{
    public AnimationController _animationController;
    public State _state;
    public Transform[] pointPatrol;
    private View _view;
    public PhotonView _photonView;
    public NavMeshAgent _agent;
    public RaycastHit[] _hits = default;
    public ActionController _actionController;
    public float delay = 5f;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _photonView = GetComponent<PhotonView>();
        _view = GetComponent<View>();
    }
    private void Start()
    {
        _actionController = new ActionController(_agent, this, _animationController, transform, _view);
    }




}
