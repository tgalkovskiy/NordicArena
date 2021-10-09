using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class ItemBlock : MonoBehaviourPunCallbacks
{
    public TypeBlock _TypeBlock;
    public Transform posSpawn;
    public GameObject[] prefabs;

    IEnumerator Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
           yield return new WaitForSeconds(0.05f);
           if (LevelCreator._sizeMap < 50)
           {
                int indexBlock = Random.Range(0, prefabs.Length);
                var block=  PhotonNetwork.Instantiate(prefabs[indexBlock].name, posSpawn.position, prefabs[indexBlock].transform.rotation);
                block.name = prefabs[indexBlock].name;
                LevelCreator._sizeMap += 1;
           }
        }
        
    }
}
