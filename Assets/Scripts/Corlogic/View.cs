
using UnityEngine;

public class View : MonoBehaviour
{
    public UiVievStats _statsUI;
    public Stats stats;
    public Presenter presenter;
    public StateControllers state;
    private void Awake()
    {
        Application.targetFrameRate = 90;
        presenter = new Presenter(this);
        state = GetComponent<StateControllers>();
        if (_statsUI._hpText != null)
        {
            _statsUI._hpText.text = $"{stats.hpMax} / {stats.hpMax}";
        }
    }

    public void GetDamage(float damage)
    {
        presenter.GetDamage(damage);
        state.vfxManager.PlayVFXBlood();
    }
    public void SetHp(float hp)
    {
        _statsUI._hp.fillAmount =hp/stats.hpMax;
        if (_statsUI._hpText != null)
        {
            _statsUI._hpText.text = $"{hp}/{stats.hpMax}";
        }
    }
    public void Die()
    {
       state.actionController.Die();
    }
}
