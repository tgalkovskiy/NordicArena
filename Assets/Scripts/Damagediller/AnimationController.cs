using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationController : MonoBehaviour
{
    public GameObject _weapon;
    public BoxCollider _collider;
    private Animator _animator;
    private bool _isAttack = true;
    private bool _isWeapon = false;
    private int _countHit = 0;
    private int _powerCombo = 0;
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
    public void AttackVariant(int typeAttack)
    {
        if(_isAttack)
        {
            switch (typeAttack)
            {
                case 0: StartCoroutine(ExecuteAttack("Hit")); _powerCombo +=2; break;
                case 1: StartCoroutine(ExecuteAttack("MiddleHit")); _powerCombo+=3; break;
                case 2: StartCoroutine(ExecuteAttack("PowerHit")); _powerCombo+=4; break;
                case 3: StartCoroutine(ExecuteAttack("kick")); _powerCombo += 1; break;
            }
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
        _countHit += 1;
        _isAttack = !_isAttack;
        _animator.SetTrigger(nameAttack);
        yield return new WaitForSeconds(1f);
        if (_countHit >= 3)
        {
            yield return ExecuteCombo();
        }
        _isAttack = !_isAttack;
    }
    IEnumerator ExecuteCombo()
    {
        switch (_powerCombo)
        {
            case 5: _animator.SetTrigger("Combo"); break;
            case 6: _animator.SetTrigger("Combo2"); break;
            case 7: _animator.SetTrigger("Combo3"); break;
            case 8: _animator.SetTrigger("Combo4"); break;
        }
        _powerCombo = 0;
        _countHit = 0;
        yield return null;
    }
    IEnumerator ExecuteWeapon()
    {
        yield return new WaitForSeconds(1f);
        _weapon.SetActive(!_isWeapon);
        _isWeapon = !_isWeapon;
    }
}
