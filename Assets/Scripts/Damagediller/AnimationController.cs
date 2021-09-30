using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationController : MonoBehaviour
{
    public BoxCollider _collider;
    private Animator _animator;
    private bool _isAttack = true;
    private int _countHit = 0;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(Vector3 velocity)
    {
        _animator.SetFloat("X", velocity.x);
        _animator.SetFloat("Z", velocity.z);
    }

    public void JumpAnimation()
    {
        _animator.SetTrigger("Jump");
    }
    public void AttackVariant()
    {
        if(_isAttack)
        {
            switch (_countHit)
            {
                case 0: StartCoroutine(ExecuteAttack("Hit")); _countHit += 1; break;
                case 1: StartCoroutine(ExecuteAttack("MiddleHit")); _countHit += 1; break;
                case 2: StartCoroutine(ExecuteAttack("PowerHit")); _countHit = 0; break;
            }
        }
    }
    public void OnWeapon()
    {
        _collider.enabled = true;
    }
    public void OfWeapon()
    {
       _collider.enabled = false;
    }

    IEnumerator ExecuteAttack(string nameAttack)
    {
        _isAttack = !_isAttack;
        _animator.SetTrigger(nameAttack);
        yield return new WaitForSeconds(1f);
        _isAttack = !_isAttack;
    }
}
