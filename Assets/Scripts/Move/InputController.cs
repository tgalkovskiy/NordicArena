using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using DG.Tweening;


[RequireComponent(typeof(Rigidbody))]
public class InputController : MonoBehaviour
{
    private AnimationController _animationController;
    private View _view;
    private PhotonView _photonView;
    private NavMeshAgent _agent;
    private Camera _mainCamera;
    private RaycastHit _hit = default;
    private NavMeshController _navMeshController;
    private bool _isAttack = false;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _photonView = GetComponent<PhotonView>();
        _view = GetComponent<View>();
        _navMeshController = new NavMeshController(_agent, _animationController, transform);
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _hit, 100))
                {
                    if (_hit.collider.gameObject.GetComponent<Enemy>())
                    {
                        _isAttack = true;
                        _agent.stoppingDistance += _navMeshController.distance;
                    }
                    else
                    {
                        _agent.stoppingDistance = 2;
                    }
                }
                _navMeshController.Move(_hit.point);
            }
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _animationController.MoveAnimation(0);
                _agent.Stop();
            }
            else
            {
                _agent.Resume();
                _animationController.MoveAnimation(1);
            }
            _isAttack = _navMeshController.Attack(transform.position,_hit.point,_agent.stoppingDistance,_isAttack, _animationController);
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                _view.ShowUiInventory();
            }
        }
    }
    
}