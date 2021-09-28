using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    public float _speed;
    public Animator _animator;
    private Rigidbody _plyerRb;
    private PhotonView _photonView;
    float x;
    float z;
    private float mouseX;
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
                mouseX = Input.mousePosition.normalized.x;
            }
            if (Input.GetMouseButton(1))
            {
                RotatePlayer(Input.mousePosition.normalized.x);
            }
            if(x!=0 || z != 0)
            {
                _animator.SetBool("Run", true);
            }
            else
            {
                _animator.SetBool("Run", false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger("Hit");
            }
        }
    }
    private void FixedUpdate()
    {
        if(_photonView.IsMine)
        {
            if (_plyerRb.velocity.magnitude < 5)
            {
                _plyerRb.AddRelativeForce(new Vector3(x, 0, z) * _speed/Time.fixedDeltaTime);
            }
            
            
        } 
    }
    private void RotatePlayer(float X)
    {
        if (mouseX != X)
        {
           
            if (X < mouseX)
            {
                transform.rotation *= quaternion.Euler(0,X*Time.deltaTime,0);
            }
            if (X > mouseX)
            {
                transform.rotation *= quaternion.Euler(0,-X*Time.deltaTime,0);
            }
        }
       
    }
}
