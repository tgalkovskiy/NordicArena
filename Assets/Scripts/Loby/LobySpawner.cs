using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobySpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _prefab = default;
    private void Start()
    {
        PhotonNetwork.Instantiate(_prefab.name, new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5)), Quaternion.identity);
    }
}
