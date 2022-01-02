
using UnityEngine;

public class InputController : StateControllers
{
    private PlayerView _playerView;
    private Camera _mainCamera;
    private RaycastHit _hit = default;
    private CameraControllers _cameraControllers;
    public GameObject metca;
    private GameObject posMove;
    private void Start()
    {
        _mainCamera = Camera.main;
        _playerView = GetComponent<PlayerView>();
        _cameraControllers = new CameraControllers(_playerView._cinemachine);
        actionController = new ActionController(this);
        posMove = Instantiate(metca,metca.transform.position, metca.transform.rotation);
    }
    
    private void Update()
    {
        //turn left
        if (state == State.Die) return;
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
            state = State.OnOfWeapon;
            
        }
        //getting a position for movement
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, 100))
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                     state = State.Attack;
                     actionController.Rotation(_hit.point);
                }
                else
                {
                    state = State.Move;
                    posMove.transform.position = _hit.point + metca.transform.position;
                    if (_hit.collider.gameObject.GetComponent<StateControllers>() &&
                        _hit.collider.gameObject.GetComponent<StateControllers>().state != State.Die)
                    {
                        state = State.Attack;
                    }
                    if (_hit.collider.gameObject.GetComponent<DataObj>())
                    {
                        state = State.Take;
                    }
                    actionController.GetPosition(_hit.point, _hit.collider.transform);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            _playerView.ShowUiInventory();
        }
        actionController.ActionState();
    }
    
}