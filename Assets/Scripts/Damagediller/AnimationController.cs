
using System.Collections;
using UnityEngine;

public enum TypeAttack
{
    Combat,
    Bow,
    Magic
}
public class AnimationController : MonoBehaviour
{
    public GameObject _weapon;
    private Animator animator;
    private bool _isWeapon = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void MoveAnimation(float velocity)
    {
        animator.SetFloat("Z", velocity);
    }
    public void AnimationState(StateControllers controllers)
    {
        if(controllers.state == State.Attack)
        {
            switch (controllers.view._stats.TypeAttack)
            {
                case TypeAttack.Combat:
                    StartCoroutine(ExecuteAnimation("Hit", controllers )); break;
                case TypeAttack.Bow:
                    StartCoroutine(ExecuteAnimation("HitBow", controllers)); break;
                case TypeAttack.Magic:
                    StartCoroutine(ExecuteAnimation("HitMagic", controllers)); break;
            }
        }
        if (controllers.state == State.Take)
        {
            StartCoroutine(ExecuteAnimation("Take", controllers));
        }
        if (controllers.state == State.OnOfWeapon)
        {
            ShowUnShowWeapon(controllers, _weapon);
        }
        if (controllers.state == State.Die)
        {
            animator.SetTrigger("Die");
        }
    }
    public void ShowUnShowWeapon(StateControllers controllers, GameObject gameObject)
    {
        StartCoroutine(ExecuteAnimation(!_isWeapon ? "OnArm" : "Disarm", controllers, gameObject));
    }
    
    IEnumerator ExecuteAnimation(string name, StateControllers controllers)
    {
        controllers.executeState = ExecuteState.Execute;
        animator.SetTrigger(name);
        yield return new WaitForSeconds(controllers.view._stats.coolDown);
        controllers.state = State.Idle;
        controllers.executeState = ExecuteState.NonExecute;
    }
    IEnumerator ExecuteAnimation(string name, StateControllers controllers, GameObject gameObject)
    {
        controllers.executeState = ExecuteState.Execute;
        animator.SetTrigger(name);
        _weapon.SetActive(!_isWeapon);
        _isWeapon = !_isWeapon;
        yield return new WaitForSeconds(1);
        controllers.state = State.Idle;
        controllers.executeState = ExecuteState.NonExecute;
    }
}
