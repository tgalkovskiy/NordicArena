using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    public float _speed;
    public Animator _animator;
    private Rigidbody _plyerRb;
    float x;
    float z;
    private void Awake()
    {
        _plyerRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        if (x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        z = Input.GetAxis("Vertical");
        if (z < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (z > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(x!=0 || z != 0)
        {
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }
    }
    private void FixedUpdate()
    {
        if (_plyerRb.velocity.magnitude < 5)
        {
            _plyerRb.AddForce(new Vector3(x, 0, z) * _speed/Time.fixedDeltaTime);
        }
        
    }
}
