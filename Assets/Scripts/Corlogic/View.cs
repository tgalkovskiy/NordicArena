
using System;
using UnityEngine;
using UnityEngine.AI;

public class View : MonoBehaviour
{
    public UiVievStats statsUI;
    public StateControllers statePerson;
    public Stats stats;
    public AnimationController animationController;
    public DamageDiller damageDiller;
    public Transform[] pointPatrol;
    public NavMeshAgent agent;
    public VFXManager vfxManager;
    public Presenter presenter;
    private void Awake()
    {
        Application.targetFrameRate = 90;
        agent = GetComponent<NavMeshAgent>();
        damageDiller = GetComponent<DamageDiller>();
        vfxManager = GetComponent<VFXManager>();
        statePerson = GetComponent<StateControllers>();
        presenter = new Presenter(this);
        statePerson.InitActionController(this);
    }

    private void Start()
    {
        if (statsUI._hpText != null)
        {
            statsUI._hpText.text = $"{stats.hpMax} / {stats.hpMax}";
        }
    }

    public void GetDamage(float damage)
    {
        presenter.GetDamage(damage);
        statePerson.view.vfxManager.PlayVFXBlood();
    }
    public void SetHp(float hp)
    {
        statsUI._hp.fillAmount =hp/stats.hpMax;
        if (statsUI._hpText != null)
        {
            statsUI._hpText.text = $"{hp}/{stats.hpMax}";
        }
    }
    public void Die()
    {
       statePerson.actionController.Die();
    }
}
