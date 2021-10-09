using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemBlock : MonoBehaviour
{
    public TypeBlock _TypeBlock;
    public Transform posSpawn;
    public GameObject[] prefabs;

    IEnumerator Start()
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
