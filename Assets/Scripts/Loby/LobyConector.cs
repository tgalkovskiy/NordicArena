
using UnityEngine;
using Photon.Pun;

public class LobyConector : MonoBehaviourPunCallbacks
{
    public string _name;
    public string _nameRoom;
    private void Awake()
    {
        PhotonNetwork.NickName = SystemInfo.deviceName;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to MasterServer");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_nameRoom, new Photon.Realtime.RoomOptions { MaxPlayers = 10 });
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_nameRoom);
    }
}
