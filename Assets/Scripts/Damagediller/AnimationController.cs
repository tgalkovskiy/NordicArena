using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TypeAttack
{
    Combat,
    Bow,
    Magic
}
public class AnimationController : MonoBehaviour
{
    public GameObject _weapon;
    public ParticleSystem _hit;
    private Animator _animator;
    private bool _isWeapon = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(float velocity)
    {
        _animator.SetFloat("Z", velocity);
    }
    public void AnimationState(StateControllers _controllers)
    {
        if(_controllers._state == State.Attack)
        {
            switch (_controllers._view._stats._TypeAttack)
            {
                case TypeAttack.Combat:
                    StartCoroutine(ExecuteAnimation("Hit", _controllers )); break;
                case TypeAttack.Bow:
                    StartCoroutine(ExecuteAnimation("HitBow", _controllers)); break;
                case TypeAttack.Magic:
                    StartCoroutine(ExecuteAnimation("HitMagic", _controllers)); break;
            }
        }
        if (_controllers._state == State.Take)
        {
            StartCoroutine(ExecuteAnimation("Take", _controllers));
        }
        
    }
    public void ShowUnShowWeapon()
    {
        if (!_isWeapon)
        {
            _animator.SetTrigger("OnArm");
        }
        else
        {
            _animator.SetTrigger("Disarm");
        }
        StartCoroutine(ExecuteWeapon());
    }
    
    IEnumerator ExecuteAnimation(string name, StateControllers _controllers)
    {
        _controllers._state = State.Stay;
        _animator.SetTrigger(name);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length/2);
    }
    IEnumerator ExecuteWeapon()
    {
        yield return new WaitForSeconds(1f);
        _weapon.SetActive(!_isWeapon);
        _isWeapon = !_isWeapon;
    }
    IEnumerator ExecuteSplash()
    {
        yield return new WaitForSeconds(0.3f);
        _hit.Play();
    }
}
