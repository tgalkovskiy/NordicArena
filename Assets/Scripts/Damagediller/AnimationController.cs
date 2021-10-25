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
    public static AnimationController Instance;
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
        Instance = this;
    }

    public void MoveAnimation(float velocity)
    {
        //_animator.SetFloat("X", velocity);
        _animator.SetFloat("Z", velocity);
    }

    public void JumpAnimation()
    {
        _animator.SetTrigger("Jump");
    }
    public void AttackVariant(TypeAttack typeAttack)
    {
        if (_isAttack)
        {
            switch (typeAttack)
            {
                case TypeAttack.Combat:
                    StartCoroutine(ExecuteCombat("Hit"));
                    break;
                case TypeAttack.Bow:
                    StartCoroutine(ExecuteCombat("HitBow"));
                    break;
                case TypeAttack.Magic:
                    StartCoroutine(ExecuteCombat("HitMagic"));
                    break;
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

    IEnumerator ExecuteCombat(string name)
    {
        _isAttack = !_isAttack;
        _animator.SetTrigger(name);
        yield return new WaitForSeconds(1f);
        _isAttack = !_isAttack;
    }
    IEnumerator ExecuteWeapon()
    {
        yield return new WaitForSeconds(1f);
        _weapon.SetActive(!_isWeapon);
        _isWeapon = !_isWeapon;
    }
}
