using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter
{
    private Model _modelPlayer;
    private View _view;
    public Presenter(CollisionDetected _collisionDetected, View _view)
    {
        _modelPlayer = new Model(100);
        this._view = _view;
        _collisionDetected._setDamage += _modelPlayer.HitDamage;
        _modelPlayer._hpUpdated += this._view.SetHp;
    }
}
