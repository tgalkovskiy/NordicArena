using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class SlaveBlock : Block
{
    public GameObject[] prefabsBlocks;
    public Transform[] posSpawnBlock;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        if (PoolContainer.poolBlocks.Count < 50)
        {
            var obj = prefabsBlocks[Random.Range(0, prefabsBlocks.Length)];
        }
        
        //BuilderBlocks(obj, );
    }
}
