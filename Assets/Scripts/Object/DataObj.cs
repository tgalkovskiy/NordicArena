using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DataObj : MonoBehaviourPunCallbacks
{
    public string _name;
    public string _description;
    public Sprite _ImageItem;
    public InventoryEnum _type;
    public CellData _Data;
    
    private void OnValidate()
    {
        _Data.name = _name;
        _Data.description = _description;
        _Data.imageItem = _ImageItem;
        _Data.type = _type;
    }
}
