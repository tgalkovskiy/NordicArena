using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviourPunCallbacks
{
    public Slider _Hp;
    public int _ID;
    private Presenter _playerPresenter;
    private PhotonView _photonView;
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _playerPresenter = new Presenter(GetComponent<CollisionDetected>(), this);
    }
    public void SetHp(int hp)
    {
        if (_Hp != null)
        {
            _Hp.value = hp;
            Debug.Log(hp); 
        }
    }
}
