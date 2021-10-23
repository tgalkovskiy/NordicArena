using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using DG.Tweening;


[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    [SerializeField] private AnimationController _AnimationController;
    [SerializeField] private View _view;
    private PhotonView _photonView;
    private NavMeshAgent _agent;
    private Camera _mainCamera;
    private RaycastHit hit = default;
    private WeaponController _weaponController;
    private bool isAttack = false;
    

    //public float _speed;
    //public float _forceJump;
    //private Rigidbody _playerRb;
    //float x;
    //float z;
    //private bool _isJump = false;
    //private Vector3 _velocity;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
        //_playerRb = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _view = GetComponent<View>();
        _weaponController = new WeaponController();
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100))
                {
                    _agent.SetDestination(hit.point);
                    if (hit.collider.gameObject.GetComponent<Enemy>())
                    {
                        isAttack = true;
                        _agent.stoppingDistance += _weaponController.distance;
                    }
                    else
                    {
                        _agent.stoppingDistance = 2;
                    }
                }
            }
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _AnimationController.MoveAnimation(0);
                _agent.Stop();
            }
            else
            {
                _agent.Resume();
                _AnimationController.MoveAnimation(1);
            }
            isAttack = _weaponController.Attack(transform.position,hit.point,_agent.stoppingDistance,isAttack, _AnimationController);
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                _view.ShowUiInventory();
            }
        }
    }
    
}