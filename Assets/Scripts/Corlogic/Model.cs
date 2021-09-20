using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model 
{
    public Model(int HP)
    {
        _Hp = HP;
    }
    private int _Hp;

    public void HitDamage(int damage)
    {
        _Hp -= damage;
    }
}
