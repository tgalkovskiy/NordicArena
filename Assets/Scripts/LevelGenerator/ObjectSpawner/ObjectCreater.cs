using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectCreater : MonoBehaviourPunCallbacks
{
    public GameObject trap;
    public GameObject enemy;
    public GameObject loot;

    public void CreateObj(TypeObject _typeObject)
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
    }
}
