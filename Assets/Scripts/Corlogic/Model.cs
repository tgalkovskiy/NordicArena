using System;
using UnityEngine;

public class Model 
{
    public Action<float> _hpUpdated;
    public Action Die;
    private Stats _stats;
    public Model(Stats stats)
    {
        _stats = stats;
    }
    public void GetDamage(float damage)
    {
        _stats._hpNow -= damage - _stats._armor;
        _hpUpdated?.Invoke(_stats._hpNow);
        if (_stats._hpNow <= 0)
        {
            Die?.Invoke();
        }
    }
}
