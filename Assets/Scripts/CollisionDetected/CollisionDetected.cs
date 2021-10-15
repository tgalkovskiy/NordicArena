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
    public Action<CellData> _getData;

    private DataObj _collisionObj;
    private void OnCollisionEnter(Collision other)
    {
        /*if (!other.gameObject?.GetComponent<DamageControl>())
        {
            return;
        }*/
        if (other.gameObject?.GetComponentInParent<View>()?._ID != _ID)
        {
            _setDamage.Invoke(10);
        }
        if (other.gameObject?.GetComponent<DataObj>())
        {
            Debug.Log(other.gameObject.name);
            _collisionObj = other.gameObject.GetComponent<DataObj>();
            _getData.Invoke(other.gameObject.GetComponent<DataObj>()._Data);
        }
    }
    public void DeleteCollisionObj()
    {
        Debug.Log(_collisionObj.name);
        Destroy(_collisionObj.gameObject);
    }
}
