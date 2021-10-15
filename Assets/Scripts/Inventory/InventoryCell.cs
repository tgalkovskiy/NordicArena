using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using WebSocketSharp;

public struct CellData
{
    public int Id;
    public string name;
    public string description;
}
public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int _id;
    public string _name;
    public Image _imageCell;
    public Text _descriptionUI;
    
    public void SetParameters(CellData cellData)
    {
        _id = cellData.Id; _name = cellData.name; _descriptionUI.text = cellData.description;
        Debug.Log(cellData.Id);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_name.IsNullOrEmpty())
        {
            _descriptionUI.gameObject.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_name.IsNullOrEmpty())
        {
           _descriptionUI.gameObject.SetActive(false); 
        }
    }
}
