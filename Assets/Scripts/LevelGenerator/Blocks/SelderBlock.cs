using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class SelderBlock : Block
{
    public GameObject[] prefabsBlocks;
    public Transform posSpawnBlock;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        var obj = prefabsBlocks[Random.Range(0, prefabsBlocks.Length)];
        BuilderBlocks(obj, posSpawnBlock);
    }
}
