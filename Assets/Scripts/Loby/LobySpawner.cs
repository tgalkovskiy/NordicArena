using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class LobySpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private CinemachineVirtualCamera _cinemachine;
    [SerializeField] private GameObject _prefab = default;
    private void Start()
    {
       var player = PhotonNetwork.Instantiate(_prefab.name, new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5)), Quaternion.identity);
        _cinemachine.Follow = player.transform;
        _cinemachine.LookAt = player.transform;
    }
}
