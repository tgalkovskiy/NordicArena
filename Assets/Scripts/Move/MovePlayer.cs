using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    [SerializeField]private AnimationController _AnimationController;
    public float _speed;
    public float _forceJump;
    private Rigidbody _plyerRb;
    private PhotonView _photonView;
    float x;
    float z;
    private float _mouseX;
    private bool _isJump = false;
    private Vector3 _velocity;
    private void Awake()
    {
        _plyerRb = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (_photonView.IsMine)
        {
            _velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (Input.GetMouseButtonDown(1))
            {
                _mouseX = Input.mousePosition.normalized.x;
            }
            if (Input.GetMouseButton(1))
            {
                RotatePlayer(Input.mousePosition.normalized.x);
            }
            if (_velocity.x != 0 || _velocity.z != 0)
            {
                _AnimationController.MoveAnimation(_velocity);
            }
            else
            {
                _AnimationController.MoveAnimation(Vector3.zero);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _AnimationController.AttackVariant();
                /*if (!_attackBlocked)
                { 
                    _animator.SetTrigger("Hit");
                    _attackBlocked = true;
                    StartCoroutine(AttackDelay());
                }*/
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJump = !_isJump;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                
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
        if (_mouseX != X)
        {
            if (X < _mouseX)
            {
                transform.rotation *= quaternion.Euler(0, -X * Time.deltaTime, 0);
            }
            if (X > _mouseX)
            {
                transform.rotation *= quaternion.Euler(0, X * Time.deltaTime, 0);
            }
        }
    }
    
    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.3f);
        _plyerRb.AddForce(Vector3.up*_forceJump / Time.fixedDeltaTime);
    }

    

       

}
