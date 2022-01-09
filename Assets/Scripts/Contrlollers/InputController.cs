
using UnityEngine;

public class InputController : StateControllers
{
    private PlayerView _playerView;
    public Camera mainCamera;
    public Camera miniMapCamera;
    private RaycastHit hit;
    private CameraControllers _cameraControllers;
    public GameObject metca;
    private GameObject posMove;
    private void Start()
    {
        _playerView = GetComponent<PlayerView>();
        _cameraControllers = new CameraControllers(_playerView._cinemachine, miniMapCamera);
        actionController = new ActionController(this);
        posMove = Instantiate(metca,metca.transform.position, metca.transform.rotation);
    }
    
    private void Update()
    {
        //turn left
        if (stateLife == StateLife.Dead) return;
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
            typeAction = TypeAction.OnOfWeapon;
            
        }
        //getting a position for movement
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    typeAction = TypeAction.AttackToStay;
                }
                else
                {
                    typeAction = TypeAction.Move;
                    posMove.transform.position = hit.point + metca.transform.position;
                    if (hit.collider.gameObject.GetComponent<StateControllers>() &&
                        hit.collider.gameObject.GetComponent<StateControllers>().stateLife != StateLife.Dead)
                    {
                        typeAction = TypeAction.Attack;
                    }
                    if (hit.collider.gameObject.GetComponent<DataObj>())
                    {
                        typeAction = TypeAction.Take;
                    }
                }
                actionController.GetTarget(hit);
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