using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DataObj : MonoBehaviourPunCallbacks
{
    public string _name;
    public int _id;
    public string _description;
    public CellData _Data;
    
    private void OnValidate()
    {
        _Data.name = _name;
        _Data.Id = _id;
        _Data.description = _description;
    }

    /*public DataObj(TypeObject _typeObject)
    {
        
    }*/
    /*public void CreateObj(TypeObject _typeObject)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (_typeObject._contentType == ContentType.enemie)
            {
                PhotonNetwork.Instantiate(enemy.name, _typeObject._possition, Quaternion.Euler(_typeObject._ratation));
            }
            if (_typeObject._contentType == ContentType.loot)
            {
                PhotonNetwork.Instantiate(loot.name, _typeObject._possition, Quaternion.Euler(_typeObject._ratation));
            }
            if (_typeObject._contentType == ContentType.trap)
            {
                PhotonNetwork.Instantiate(trap.name, _typeObject._possition, Quaternion.Euler(_typeObject._ratation));
            }
        }
    }*/
}
