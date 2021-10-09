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
           if (LevelCreator._sizeMap < 100)
           {
            int indexBlock = Random.Range(0, prefabs.Length);
            Instantiate(prefabs[indexBlock], posSpawn.position, prefabs[indexBlock].transform.rotation);
            LevelCreator._sizeMap += 1;
           }
        }
        
    }
}
