using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class View : MonoBehaviourPunCallbacks
{
    public UIView _uIView;
    public Slider _Hp;
    public CinemachineVirtualCamera cinemachine;
    public int _ID;
    public GameObject VFX;
    private Presenter _playerPresenter;
    private PhotonView _photonView;
    private CollisionDetected _collisionDetected;
    
    private void Awake()
    {
        Application.targetFrameRate = 90;
        _photonView = GetComponent<PhotonView>();
        _collisionDetected = GetComponent<CollisionDetected>();
        _playerPresenter = new Presenter(_collisionDetected, this);
    }
    public void SetHp(int hp)
    {
        if (_Hp != null)
        {
            _Hp.value = hp;
            Debug.Log(hp); 
        }
    }
    public void ShowUiInventory()
    {
        _uIView.ShowInventoryPanel();
    }
    public void SetDataCell(CellData cellData)
    {
        if(_uIView.SetCell(cellData))
        {
            _collisionDetected.DeleteCollisionObj();
        }
    }
}
