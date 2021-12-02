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
    private CellData _data;
    private UIViewInventory _uiViewInventory;
    private GameObject viewSpawnCell;
    public void SetParameters(CellData cellData, UIViewInventory viewInventory)
    {
        _uiViewInventory = viewInventory;
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
        viewSpawnCell.transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        viewSpawnCell = Instantiate(_uiViewInventory._spawnObj, transform.position, Quaternion.identity, _uiViewInventory.transform);
        viewSpawnCell.transform.GetChild(0).GetComponent<Image>().sprite = _data.Image;
        viewSpawnCell.GetComponent<Image>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(viewSpawnCell.gameObject);
        if (_uiViewInventory.DragCell == null) return;
        if (_uiViewInventory.DragCell._InventoryEnum == _data.type )
        {
            if (_uiViewInventory.DragCell.transform.childCount > 0)
            {
                _uiViewInventory.DragCell.transform.GetChild(0).parent = _uiViewInventory._cellContainer;
            }
            transform.parent = _uiViewInventory.DragCell.transform;
        }
        if(_uiViewInventory.DragCell._InventoryEnum == InventoryEnum.Inventory)
        {
            transform.parent = _uiViewInventory.DragCell.transform;
        }
    }
}
