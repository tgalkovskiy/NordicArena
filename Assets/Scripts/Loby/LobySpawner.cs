
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LobySpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private CinemachineVirtualCamera cinemachine = default;
    [SerializeField] private GameObject _prefab = default;

    private void Awake()
    {
        var player = Instantiate(_prefab, new Vector3(Random.Range(-1, 1), 1, Random.Range(-1, 1)), Quaternion.identity);
        player.GetComponent<PlayerView>()._cinemachine = cinemachine;
        cinemachine.Follow = player.transform;
        cinemachine.LookAt = player.transform;
    }
    public override void OnJoinedRoom()
    {
       
    }
    public override void OnLeftRoom()
    {
        
    }
}
