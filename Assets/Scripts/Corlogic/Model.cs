using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model 
{
    public Action<int> _hpUpdated;
    public Model(int HP)
    {
        _Hp = HP;
    }
    private int _Hp;

    public void HitDamage(int damage)
    {
        _Hp -= damage;
        _hpUpdated.Invoke(_Hp);
    }
}
