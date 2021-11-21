using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter
{
    private Model _model;
    private View _view;
    public Presenter(View view)
    {
        _view = view;
        _model = new Model(_view._stats);
        _model._hpUpdated += _view.SetHp;
        _model.Die += _view.Die;
    }
    public void GetDamage(float damage)
    {
        _model.GetDamage(damage);
    }
}
