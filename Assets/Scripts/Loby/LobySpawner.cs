
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;

public class LobySpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private CinemachineVirtualCamera _cinemachine = default;
    [SerializeField] private Slider _hpSlider = default;
    [SerializeField] private GameObject _prefab = default;
    private Dictionary<int, Photon.Realtime.Player> Players;
    private void Start()
    {
       var player = PhotonNetwork.Instantiate(_prefab.name, new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5)), Quaternion.identity);
        player.GetComponent<View>()._ID = player.GetComponent<PhotonView>().ViewID;
        player.GetComponent<View>()._Hp = _hpSlider; 
        _cinemachine.Follow = player.transform;
       _cinemachine.LookAt = player.transform;
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
