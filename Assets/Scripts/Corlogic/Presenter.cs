using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter
{
    private Model _model;
    private View _view;
    public Presenter(View view)
    {
        _model = new Model(100);
        _view = view;
        _model._hpUpdated += _view.SetHp;
    }
    public void GetDamage(float damage)
    {
        _model.GetDamage(damage);
    }
}
