using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetected : MonoBehaviour
{
    private int _ID;
    private void Start()
    {
        _ID = GetComponent<View>()._ID;
    }

    public Action<int> _setDamage; 
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject?.GetComponent<DamageControl>())
        {
            return;
        }
        if (other.gameObject?.GetComponentInParent<View>()?._ID != _ID)
        {
            _setDamage.Invoke(10);
        }
 
    }
}
