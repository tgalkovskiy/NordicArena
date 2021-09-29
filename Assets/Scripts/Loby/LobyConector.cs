
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class LobyConector : MonoBehaviourPunCallbacks
{
    public string _name;
    public string _nameRoom;
    [SerializeField] private Text _errorText;
    private bool _isExists = false;
    private TypedLobby _lobby;
    private const string NAME_PROP_KEY = "C0";
    private void Awake()
    {
        _lobby = new TypedLobby(_nameRoom, LobbyType.SqlLobby);
        PhotonNetwork.NickName = SystemInfo.deviceName;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        CheckRooms();
        Debug.Log("Connect to MasterServer");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        _isExists = false;
        _errorText.text = "";
        foreach(RoomInfo Room in roomList) {
            if (Room.Name == _nameRoom) {
                _isExists = true;
            }
        }
    }
    public void CreateRoom()
    {
        CheckRooms();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        roomOptions.CustomRoomProperties = new Hashtable { { NAME_PROP_KEY, _nameRoom } };
        roomOptions.CustomRoomPropertiesForLobby = new string[]{ NAME_PROP_KEY };
        PhotonNetwork.CreateRoom(_nameRoom, roomOptions, _lobby);

        _errorText.text = _isExists ? "Room already exists" : "";
    }
    public void JoinRoom()
    {
        CheckRooms();
        PhotonNetwork.JoinRoom(_nameRoom);

        _errorText.text = _isExists ? "" : "Room not exists";
    }

    private void CheckRooms() {
        PhotonNetwork.GetCustomRoomList(_lobby, NAME_PROP_KEY + " = '" + _nameRoom + "'");
    }
}
