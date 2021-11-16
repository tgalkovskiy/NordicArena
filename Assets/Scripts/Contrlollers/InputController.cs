
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public class InputController : StateControllers
{
    private PlayerView _playerView;
    private Camera _mainCamera;
    private RaycastHit _hit = default;
    private CameraControllers _cameraControllers;
    private void Awake()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _photonView = GetComponent<PhotonView>();
        _playerView = GetComponent<PlayerView>();
        
    }
    private void Start()
    {
        _cameraControllers = new CameraControllers(_playerView.cinemachine);
        _actionController = new ActionController(_agent, this, _animationController, transform, _playerView);
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                _animationController.ShowUnShowWeapon();
            }
            //getting a position for movement
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _hit, 100))
                {
                    _state = State.Move;
                    if (_hit.collider.gameObject.GetComponent<MonstersView>())
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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _actionController._TypeAttack = TypeAttack.Combat;
                _actionController.distanceSkill = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _actionController._TypeAttack = TypeAttack.Bow;
                _actionController.distanceSkill = 9;
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                _actionController._TypeAttack = TypeAttack.Magic;
                _actionController.distanceSkill = 6;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                _playerView.ShowUiInventory();
            }
            _actionController.ActionState();
        }
    }
    
}