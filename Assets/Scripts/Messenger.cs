using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public  class Messenger 
{
    public static PhotonView _photonView;

    public static void SendMessage(string message)
    {
        _photonView.RPC("Test", RpcTarget.All, message);
    }

    [PunRPC]
    public void GetMessage(string message)
    {
        Debug.Log(message);
    }
}
