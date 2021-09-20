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
        _collisionDetected._setDamage += HitDamage;
        this._view = _view;
        
    }
    private void HitDamage(int damage)
    {
        _modelPlayer.HitDamage(damage);
        
    }
}
