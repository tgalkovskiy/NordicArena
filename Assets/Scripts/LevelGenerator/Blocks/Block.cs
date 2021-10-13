using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class Block : MonoBehaviourPunCallbacks, IBlockBuilder
{
    public void BuilderBlocks(GameObject gameObject, Transform pos)
    {
        var spawnObj = PhotonNetwork.Instantiate("Bloks/" + gameObject.name, pos.position, gameObject.transform.rotation);
        spawnObj.name = gameObject.name;
        PoolContainer.poolBlocks.Add(spawnObj);
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




