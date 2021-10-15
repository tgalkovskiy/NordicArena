using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model 
{
    public Action<int> _hpUpdated;
    public Action<CellData> _cellData;
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
    public void CreateDataCell(CellData data)
    {
        Debug.Log("createCell");
        _cellData.Invoke(data);
    }
}
