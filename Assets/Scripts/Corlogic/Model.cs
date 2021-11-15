using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model 
{
    public Action<float> _hpUpdated;
    private float _Hp;
    public Model(int HP)
    {
        _Hp = HP;
    }
    public void GetDamage(float damage)
    {
        _Hp -= damage;
        _hpUpdated.Invoke(_Hp);
        Debug.Log(_Hp);
    }
}
