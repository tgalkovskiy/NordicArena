using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    public string Message;
    public float _speed;
    public float _forceJump;
    public Animator _animator;
    public Animation _animation;
    private Rigidbody _plyerRb;
    private PhotonView _photonView;
    float x;
    float z;
    private bool _attackBlocked;
    private float _mouseX;
    private bool _isJump = false;
    private void Awake()
    {
        _plyerRb = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (_photonView.IsMine)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            if (Input.GetMouseButtonDown(1))
            {
                _mouseX = Input.mousePosition.normalized.x;
            }
            if (Input.GetMouseButton(1))
            {
                RotatePlayer(Input.mousePosition.normalized.x);
            }
            if (x != 0 || z != 0)
            {
                _animator.SetBool("Run", true);
            }
            else
            {
                _animator.SetBool("Run", false);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!_attackBlocked)
                { 
                    _animator.SetTrigger("Hit");
                    _attackBlocked = true;
                    StartCoroutine(AttackDelay());
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJump = !_isJump;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                SendMessage(Message);
            }
        }
    }
    private void FixedUpdate()
    {
        if (_photonView.IsMine)
        {
            if (_plyerRb.velocity.magnitude < 5)
            {
                _plyerRb.AddRelativeForce(new Vector3(x, 0, z) * _speed / Time.fixedDeltaTime);
            }
            if (_isJump)
            {
                _plyerRb.AddForce(Vector3.up*_forceJump / Time.fixedDeltaTime);
                _isJump = !_isJump;
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
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1f);
        _attackBlocked = false;

    }

    private void SendMessage(string message)
    {
        _photonView.RPC("GetMessage", RpcTarget.All, message);
    }
    [PunRPC]
    public void GetMessage(string message)
    {
        Debug.Log(message);
    }

       

}
