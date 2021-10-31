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
    public GameObject VFX;
    public AnimationController _animationController;
    private View _view;
    private PhotonView _photonView;
    private NavMeshAgent _agent;
    private Camera _mainCamera;
    private RaycastHit _hit = default;
    private NavMeshController _navMeshController;
    private CameraControllers _cameraControllers;
    private bool _isAttack = false;
    private TypePosition _typePosition = TypePosition.DefaultPos;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _photonView = GetComponent<PhotonView>();
        _view = GetComponent<View>();
    }
    private void Start()
    {
        _cameraControllers = new CameraControllers(_view.cinemachine);
        VFX = _view.VFX;
        _navMeshController = new NavMeshController(_agent, _animationController, transform, VFX);
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
                    _typePosition = TypePosition.DefaultPos;
                    if (_hit.collider.gameObject.GetComponent<Enemy>())
                    {
                        _typePosition = TypePosition.AttackPos;
                    }
                }
                _navMeshController.GetPosition(_hit.point, _typePosition, _hit.collider.transform);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                _view.ShowUiInventory();
            }
            _navMeshController.NawMeshState();
        }
    }
    
}