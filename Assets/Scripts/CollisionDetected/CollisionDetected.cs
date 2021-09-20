using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetected : MonoBehaviour
{
    public Action<int> _setDamage; 
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            _setDamage.Invoke(10);
        }
    }
}
