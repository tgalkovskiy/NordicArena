using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    [SerializeField]private AnimationController _AnimationController;
    [SerializeField] private View _view;
    private PhotonView _photonView;
    private NavMeshAgent _agent;
    private Camera _mainCamera;

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
    }
    private void Update()
    {
        if (_photonView.IsMine)
        {
            /*//_velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
           // if (_velocity.x != 0 || _velocity.z != 0)
            {
                _AnimationController.MoveAnimation(_velocity);
            }
            else
            {
                _AnimationController.MoveAnimation(Vector3.zero);
            }*/
            /*if (Input.GetKey(KeyCode.Q))
            {
                _view.cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value = 20f;
            }
            if (Input.GetKey(KeyCode.E))
            {
                _view.cinemachine.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value = -20f;
            }*/
            /*if (Input.GetKeyDown(KeyCode.R))
            {
                _AnimationController.ShowUnShowWeapon();
            }*/
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    _agent.SetDestination(hit.point);
                    if (hit.collider.gameObject.GetComponent<Enemy>())
                    {
                        
                    }
                }
                    //_AnimationController.AttackVariant(0);
            }
            if(_agent.remainingDistance < _agent.stoppingDistance)
            {
                _AnimationController.MoveAnimation(0);
                _agent.Stop();
            }
            else
            {
                _agent.Resume();
                _AnimationController.MoveAnimation(1);
            }
            /*if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _AnimationController.AttackVariant(1);
            }*/
            /*if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                _AnimationController.AttackVariant(2);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                _AnimationController.AttackVariant(3);
            }*/
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJump = !_isJump;
            }*/
            if (Input.GetKeyDown(KeyCode.I))
            {
                _view.ShowUiInventory();
            }
           
           
        }
    }
    /*private void FixedUpdate()
    {
        if (_photonView.IsMine)
        {
            if (_plyerRb.velocity.magnitude < 5)
            {
                _plyerRb.AddRelativeForce(_velocity * _speed / Time.fixedDeltaTime);
            }
            if (_isJump)
            {
                _isJump = !_isJump;
                _AnimationController.JumpAnimation();
                StartCoroutine(JumpDelay());
            }
        }
    }*/
    private void RotatePlayer(float X)
    {
        transform.rotation *= Quaternion.Euler(0, X * Time.deltaTime*50, 0);
    }
    /*IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.3f);
        _plyerRb.AddForce(Vector3.up*_forceJump / Time.fixedDeltaTime);
    }*/
    private void AttackAnimation()
    {
        _AnimationController.AttackVariant(0);
    }
    

       

}
