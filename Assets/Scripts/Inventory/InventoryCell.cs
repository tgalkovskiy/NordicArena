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
    public Sprite Image;
    public InventoryEnum type;
}
public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private int _id;
    public string _name;
    public int _count;
    public Image _imageCell;
    public Text _descriptionUI;
    private Transform _inventoryParent;
    private Transform _cellParent;
    private CellData _data;
    private UIViewInventory _uiViewInventory;
    
    public void Init(Transform _containerInventory, Transform _containerCell, UIViewInventory viewInventory)
    {
        _inventoryParent = _containerInventory;
        _cellParent = _containerCell;
        _uiViewInventory = viewInventory;
    }
    public void SetParameters(CellData cellData)
    {
        _data = cellData;
        _id = cellData.Id; 
        _name = cellData.name; 
        _descriptionUI.text = cellData.description;
        if (cellData.Image != null) { _imageCell.sprite = cellData.Image;}
        _data.type = cellData.type;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        //transform.parent = _inventoryParent;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //transform.parent = _cellParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_name.IsNullOrEmpty())
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_name.IsNullOrEmpty())
        {
            transform.parent = _inventoryParent;
            transform.GetComponent<Image>().raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!_name.IsNullOrEmpty())
        {
            if (_uiViewInventory.DragCell !=null && _uiViewInventory.DragCell._InventoryEnum == _data.type)
            {
                transform.parent = _uiViewInventory.DragCell.transform;
            }
            else
            {
                int index = 0;
                for (int i = 0; i < _cellParent.transform.childCount; i++)
                {
                    if (Vector3.Distance(transform.position, _cellParent.GetChild(i).position) <
                        Vector3.Distance(transform.position, _cellParent.GetChild(index).position))
                    {
                        index = i;
                    }
                }
                transform.parent = _cellParent;
                transform.SetSiblingIndex(index);
            } 
            transform.GetComponent<Image>().raycastTarget = true;
            if (_cellParent.transform.childCount < 44)
            {
                _uiViewInventory.CreateCellInventory(1);
            }
            if (_cellParent.transform.childCount > 44)
            {
                _uiViewInventory.DeleteInventoryCell();
            }
        }
    }
}
