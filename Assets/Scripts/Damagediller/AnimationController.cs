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
    public void AttackVariant(TypeAttack typeAttack)
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
    public void TakeItem()
    {
        _animator.SetTrigger("Take");
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
    
    IEnumerator ExecuteCombat(string name)
    {
        _animator.SetTrigger(name);
        yield return null;
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
