using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    public float _speed;
    public Animator _animator;
    private Rigidbody _plyerRb;
    private PhotonView _photonView;
    float x;
    float z;
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
        if (_photonView.IsMine)
        {
            RotatePlayer(x, z);
            if (_plyerRb.velocity.magnitude < 5)
            {
                _plyerRb.AddForce(new Vector3(x, 0, z) * _speed/Time.fixedDeltaTime);
            }
        } 
    }
    private void RotatePlayer(float x, float z)
    {
        if (z > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (x > 0)
        {
             transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (z < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (x < 0 )
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }
}
