using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class View : MonoBehaviourPunCallbacks
{
    public UiVievStats _statsUI;
    public Stats _stats;
    public Presenter _presenter;
    public PhotonView _photonView;

    private void Awake()
    {
        Application.targetFrameRate = 90;
        _photonView = GetComponent<PhotonView>();
        _presenter = new Presenter(this);
        if (_statsUI._hpText != null)
        {
            _statsUI._hpText.text = $"{_stats._hpMax} / {_stats._hpMax}";
        }
    }

    public void GetDamage(float damage)
    {
        _presenter.GetDamage(damage);
    }
    public void SetHp(float hp)
    {
        _statsUI._hp.fillAmount =hp/_stats._hpMax;
        if (_statsUI._hpText != null)
        {
            _statsUI._hpText.text = $"{hp}/{_stats._hpMax}";
        }
    }
}
