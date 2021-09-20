using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponControl : MonoBehaviour
{
    public BoxCollider _collider;
    public void OnWeapon()
    {
        _collider.enabled = true;
        Debug.Log($"on");
    }
    public void OfWeapon()
    {
       _collider.enabled = false;
        Debug.Log($"of");
    }   
}
