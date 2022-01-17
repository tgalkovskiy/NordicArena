
using UnityEngine;

public class BehaviourPlayer : MonoBehaviour
{
    public PlayerView playerView;
    private RaycastHit hit;
    private CameraControllers _cameraControllers;
    private GameObject posMove;
    private Ray ray;
    private void Start()
    {
        playerView = GetComponent<PlayerView>();
        _cameraControllers = new CameraControllers(playerView._cinemachine, playerView.miniMapCamera);
        posMove = Instantiate(playerView.indicationPosition,playerView.indicationPosition.transform.position, playerView.indicationPosition.transform.rotation);
    }
    
    private void Update()
    {
        //turn left
        if (playerView.statePerson.stateLife == StateLife.Dead) return;
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
            playerView.statePerson.typeAction = TypeAction.OnOfWeapon;
            
        }
        //getting a position for movement
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray= playerView.mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    playerView.statePerson.typeAction = TypeAction.AttackToStay;
                }
                else
                {
                    playerView.statePerson.typeAction = TypeAction.Move;
                    posMove.transform.position = hit.point + playerView.indicationPosition.transform.position;
                    if (hit.collider.gameObject.GetComponent<StateControllers>() &&
                        hit.collider.gameObject.GetComponent<StateControllers>().stateLife != StateLife.Dead)
                    {
                        playerView.statePerson.typeAction = TypeAction.Attack;
                    }
                    if (hit.collider.gameObject.GetComponent<DataObj>())
                    {
                        playerView.statePerson.typeAction = TypeAction.Take;
                    }
                }
                playerView.statePerson.actionController.GetTarget(hit);
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
            playerView.ShowUiInventory();
        }
        playerView.statePerson.actionController.ActionState();
    }
    
}