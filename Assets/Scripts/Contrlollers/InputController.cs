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
    public AnimationController _animationController;
    private View _view;
    private PhotonView _photonView;
    private NavMeshAgent _agent;
    private Camera _mainCamera;
    private RaycastHit _hit = default;
    private NavMeshController _navMeshController;
    private CameraControllers _cameraControllers;
    private bool _isAttack = false;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _photonView = GetComponent<PhotonView>();
        _view = GetComponent<View>();
        _navMeshController = new NavMeshController(_agent, _animationController, transform);
    }
    private void Start()
    {
        _cameraControllers = new CameraControllers(_view.cinemachine);
    }
    private void Update()
    {
        if (_photonView.IsMine)
        {
            //turn left
            if (Input.GetKey(KeyCode.Q))
            {
                _cameraControllers.TurnCameraLeft();
            }
            //turn right 
            if (Input.GetKey(KeyCode.E))
            {
                _cameraControllers.TurnCameraRight();
            }
            //return the camera to its original position 
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                _cameraControllers.ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                _cameraControllers.ReturnCameraAngleDefault();
            }
            //getting a position for movement
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
            _navMeshController.NawMeshState();
            _isAttack = _navMeshController.Attack(transform.position,_hit.point,_agent.stoppingDistance,_isAttack, _animationController);
            if (Input.GetKeyDown(KeyCode.I))
            {
                _view.ShowUiInventory();
            }
        }
    }
    
}