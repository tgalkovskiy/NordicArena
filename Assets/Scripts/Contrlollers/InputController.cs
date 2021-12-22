
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public class InputController : StateControllers
{
    private PlayerView _playerView;
    private Camera _mainCamera;
    private RaycastHit _hit = default;
    private CameraControllers _cameraControllers;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _playerView = GetComponent<PlayerView>();
        _cameraControllers = new CameraControllers(_playerView._cinemachine);
        _actionController = new ActionController(this);
    }
    
    private void Update()
    {
        /*if (_photonView.IsMine)
        {*/
            //turn left
            if(_state == State.Die) return;
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                _animationController.ShowUnShowWeapon();
            }
            //getting a position for movement
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out _hit, 100))
                    {
                        _state = State.Attack;
                    }
                    _actionController.GetPosition(_hit.point, null);
                }
                else
                {
                   Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                   if (Physics.Raycast(ray, out _hit, 100))
                   {
                       _state = State.Move;
                       if (_hit.collider.gameObject.GetComponent<StateControllers>() &&
                           _hit.collider.gameObject.GetComponent<StateControllers>()._state != State.Die)
                       {
                           _state = State.Attack;
                       }
                       if (_hit.collider.gameObject.GetComponent<DataObj>())
                       {
                           _state = State.Take;
                       }
                       _actionController.GetPosition(_hit.point, _hit.collider.transform);
                   } 
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                _playerView.ShowUiInventory();
            }
            _actionController.ActionState();
        //}
    }
    
}