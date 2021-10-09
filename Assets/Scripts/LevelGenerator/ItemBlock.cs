using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ObjectCreater))]
public class ItemBlock : MonoBehaviourPunCallbacks
{
    public TypeBlock _TypeBlock;
    public Transform posSpawn;
    public Transform posSpawnObj;
    public GameObject[] prefabs;
    private ObjectCreater _objectCreater;
    private void Awake()
    {
        _objectCreater = GetComponent<ObjectCreater>();
    }
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
                if (posSpawnObj != null)
                {
                    _objectCreater.CreateObj(new TypeObject()
                    {
                        _contentType = ContentType.loot, _possition = posSpawnObj.position,
                        _ratation = posSpawnObj.rotation.eulerAngles
                    });
                }
           }
        }
        
    }
}
