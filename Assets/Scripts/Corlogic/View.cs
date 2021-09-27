using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public int _ID;
    private Presenter _playerPresenter;
    private void Awake()
    {
        _playerPresenter = new Presenter(GetComponent<CollisionDetected>(), this);
    }

    public void SetHp(int hp)
    {
        Debug.Log(hp);
    }
}
