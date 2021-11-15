using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class View : MonoBehaviourPunCallbacks
{
    public UiVievStats _Stats;
    public Presenter _Presenter;
    public PhotonView _photonView;
    
    public void GetDamage(float damage)
    {
        _Presenter.GetDamage(damage);
    }
    public void SetHp(float hp)
    {
        if (_Stats._hp != null)
        {
            _Stats._hp.fillAmount =hp/100;
        }
    }
}
