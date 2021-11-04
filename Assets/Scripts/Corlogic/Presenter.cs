using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter
{
    private Model _modelPlayer;
    private View _view;
    public Presenter(View _view)
    {
        _modelPlayer = new Model(100);
        this._view = _view;
        _modelPlayer._hpUpdated += this._view.SetHp;
        
        //_collisionDetected._setDamage += _modelPlayer.HitDamage;
        //_collisionDetected._getData += _modelPlayer.CreateDataCell;
        //_modelPlayer._cellData += this._view.SetDataCell;
    }
    
}
