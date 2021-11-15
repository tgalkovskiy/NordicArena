using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDiller : MonoBehaviour
{
    public Transform pos;
    public View _view;
    public float sizeZoneDamage;
    private RaycastHit[] _hits;
    public void Damage()
    {
        _hits = Physics.SphereCastAll(pos.transform.position, sizeZoneDamage, Vector3.forward, sizeZoneDamage);
        foreach (var I in _hits)
        {
            if (I.collider.gameObject.GetComponent<View>() && I.collider.gameObject.GetComponent<View>()!=_view)
            {
                I.collider.gameObject.GetComponent<View>().GetDamage(10);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos.transform.position, sizeZoneDamage);
    }
}
