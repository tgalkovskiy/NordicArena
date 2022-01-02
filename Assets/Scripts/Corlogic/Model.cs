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
        _stats.hpNow -= damage - _stats.armor;
        _hpUpdated?.Invoke(_stats.hpNow);
        if (_stats.hpNow <= 0)
        {
            Die?.Invoke();
        }
    }
}
