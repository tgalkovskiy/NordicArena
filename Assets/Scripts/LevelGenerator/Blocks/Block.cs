using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class Block : MonoBehaviourPunCallbacks, IBlockBuilder
{
    public bool BuilderBlocks(GameObject gameObject, Transform pos)
    {
        var spawnObj = PhotonNetwork.Instantiate("Bloks/" + gameObject.name, pos.position, gameObject.transform.rotation);
        spawnObj.name = gameObject.name;
        for(int i = 0; i < spawnObj.transform.childCount; i++)
        {
            Transform childBlock = spawnObj.transform.GetChild(i);
            if (!childBlock.name.StartsWith("Ground_")) {
                continue;
            }
            if (IsPositionFilled(childBlock)) {
                Destroy(spawnObj);
                return false;
            }
        }
        if (IsPositionFilled(spawnObj.transform)) {
            Destroy(spawnObj);
            return false;
        }
        PoolContainer.poolBlocks.Add(spawnObj);
        return true;
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

    private bool IsPositionFilled(Transform checkPosition) {
        foreach(GameObject block in PoolContainer.poolBlocks) {
            if (block.transform.position == checkPosition.position) {
                return true;
            }
            for(int i = 0; i < block.transform.childCount; i++)
            {
                Transform childBlock = block.transform.GetChild(i);
                if (!childBlock.name.StartsWith("Ground_")) {
                    continue;
                }
                if (childBlock.position == checkPosition.position) {          
                    return true;
                }
            }
        }

        return false;
    }
}
