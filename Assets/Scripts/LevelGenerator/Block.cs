using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class Block : MonoBehaviourPunCallbacks, IBlockBuilder
{
    public Transform[] posSpawn;
    public Transform posSpawnObj;
    public GameObject[] prefabs;
    public GameObject prefabSlave;
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        BuilderBlocks();
    }

    public void BuilderBlocks()
    {
        if (LevelCreator.sizeMap < 50)
        {
            int indexBlock = Random.Range(0, prefabs.Length);
            var block = PhotonNetwork.Instantiate("Bloks/" + prefabs[indexBlock].name, posSpawn[0].position, prefabs[indexBlock].transform.rotation);
            block.name = prefabs[indexBlock].name;
            LevelCreator.sizeMap += 1;
            if (posSpawn.Length > 1)
            {
                var blockForSlave = PhotonNetwork.Instantiate("Bloks/" + prefabSlave.name, posSpawn[1].position, prefabSlave.transform.rotation);
                blockForSlave.name = prefabSlave.name;
                LevelCreator.sizeMap += 1;
            }
        }
        
    }
    public void BuilderEnemy()
    {
        throw new NotImplementedException();
    }

    public void BuilderTraps()
    {
        throw new NotImplementedException();
    }

    public void BuilderLoot()
    {
        throw new NotImplementedException();
    }
}




