
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
    private Dictionary<int, Photon.Realtime.Player> Players;
    
    private void Awake()
    { 
        Application.targetFrameRate = 90;
        var player = PhotonNetwork.Instantiate(_prefab.name, new Vector3(Random.Range(-1, 1), 1, Random.Range(-1, 1)), Quaternion.identity);
        player.GetComponent<PlayerView>().cinemachine = cinemachine;
        cinemachine.Follow = player.transform;
        cinemachine.LookAt = player.transform;
    }
    public override void OnJoinedRoom()
    {
        Players = PhotonNetwork.CurrentRoom.Players;
        Debug.Log(Players.Count);
    }
    public override void OnLeftRoom()
    {
        
    }
}
