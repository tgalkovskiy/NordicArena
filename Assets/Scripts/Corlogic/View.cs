using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    private Presenter _playerPresenter;
    private void Awake()
    {
        _playerPresenter = new Presenter();
    }
}
