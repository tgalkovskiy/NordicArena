
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WebSocketSharp;

[Serializable]
public class CellData
{
    public int Id;
    public string name;
    public string description;
    public Sprite imageItem;
    public InventoryEnum type;
}
public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private int _id;
    public InventoryEnum inventoryEnum;
    public string nameItem;
    public Image imageCell;
    public Sprite defaultImageCell;
    public Text description;
    public CellData data;
    private UIViewInventory uiViewInventory;
    private GameObject viewSpawnCell;

    public void Init(UIViewInventory viewInventory)
    {
        uiViewInventory = viewInventory;
    }
    
    public void SetDataCell(CellData cellData)
    {
        data = cellData;
        _id = cellData.Id; 
        nameItem = cellData.name; 
        description.text = cellData.description;
        if (cellData.imageItem != null) { imageCell.sprite = cellData.imageItem;}
        data.type = cellData.type;
    }
    private void ClearDataCell()
    {
        nameItem = null; 
        description.text = null;
        data = null;
        imageCell.sprite = defaultImageCell;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        uiViewInventory.DragCell = this;
        if (!nameItem.IsNullOrEmpty())
        {
            description.gameObject.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        uiViewInventory.DragCell = null;
        if (!nameItem.IsNullOrEmpty())
        {
           description.gameObject.SetActive(false);
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
        viewSpawnCell = Instantiate(uiViewInventory._spawnObj, transform.position, Quaternion.identity, uiViewInventory.transform);
        viewSpawnCell.transform.GetChild(0).GetComponent<Image>().sprite = data.imageItem;
        viewSpawnCell.GetComponent<Image>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(viewSpawnCell.gameObject);
        if (uiViewInventory.DragCell == null) return;
        if (uiViewInventory.DragCell.inventoryEnum == data.type)
        {
            uiViewInventory.DragCell.SetDataCell(data);
            ClearDataCell();
        }
        if(uiViewInventory.DragCell.inventoryEnum == InventoryEnum.Inventory)
        {
            
        }
    }
}
