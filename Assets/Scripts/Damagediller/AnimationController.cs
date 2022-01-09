
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
        if(controllers.typeAction == TypeAction.Attack || controllers.typeAction == TypeAction.AttackToStay)
        {
            switch (controllers.view.stats.TypeAttack)
            {
                case TypeAttack.Combat:
                    StartCoroutine(ExecuteAnimation("Hit", controllers )); break;
                case TypeAttack.Bow:
                    StartCoroutine(ExecuteAnimation("HitBow", controllers)); break;
                case TypeAttack.Magic:
                    StartCoroutine(ExecuteAnimation("HitMagic", controllers)); break;
            }
        }
        if (controllers.typeAction == TypeAction.Take)
        {
            StartCoroutine(ExecuteAnimation("Take", controllers));
        }
        if (controllers.typeAction == TypeAction.OnOfWeapon)
        {
            ShowUnShowWeapon(controllers, _weapon);
        }
        if (controllers.stateLife == StateLife.Dead)
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
        yield return new WaitForSeconds(controllers.view.stats.coolDown);
        controllers.typeAction = TypeAction.Idle;
        controllers.executeState = ExecuteState.NonExecute;
    }
    IEnumerator ExecuteAnimation(string name, StateControllers controllers, GameObject gameObject)
    {
        controllers.executeState = ExecuteState.Execute;
        animator.SetTrigger(name);
        _weapon.SetActive(!_isWeapon);
        _isWeapon = !_isWeapon;
        yield return new WaitForSeconds(1);
        controllers.typeAction = TypeAction.Idle;
        controllers.executeState = ExecuteState.NonExecute;
    }
}
