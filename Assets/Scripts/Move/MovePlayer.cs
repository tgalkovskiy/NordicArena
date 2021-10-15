using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    [SerializeField]private AnimationController _AnimationController;
    [SerializeField] private View _view;
    public float _speed;
    public float _forceJump;
    private Rigidbody _plyerRb;
    private PhotonView _photonView;
    float x;
    float z;
    private bool _isJump = false;
    private Vector3 _velocity;
    private void Awake()
    {
        _plyerRb = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _view = GetComponent<View>();
    }
    private void Update()
    {
        if (_photonView.IsMine)
        {
            _velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (_velocity.x != 0 || _velocity.z != 0)
            {
                _AnimationController.MoveAnimation(_velocity);
            }
            else
            {
                _AnimationController.MoveAnimation(Vector3.zero);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                RotatePlayer(-1);
            }
            if (Input.GetKey(KeyCode.E))
            {
                RotatePlayer(1);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _AnimationController.ShowUnShowWeapon();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _AnimationController.AttackVariant(0);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _AnimationController.AttackVariant(1);
            }
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                _AnimationController.AttackVariant(2);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                _AnimationController.AttackVariant(3);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJump = !_isJump;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                _view.ShowUiInventory();
            }
        }
    }
    private void FixedUpdate()
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
    }
    private void RotatePlayer(float X)
    {
        transform.rotation *= Quaternion.Euler(0, X * Time.deltaTime*50, 0);
    }
    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.3f);
        _plyerRb.AddForce(Vector3.up*_forceJump / Time.fixedDeltaTime);
    }

    

       

}
