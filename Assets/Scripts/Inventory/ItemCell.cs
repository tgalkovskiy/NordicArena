using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int _id;
    private UIViewInventory _uiViewInventory;
    public InventoryEnum _InventoryEnum;
    
    public void Init(UIViewInventory viewInventory)
    {
        _uiViewInventory = viewInventory;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _uiViewInventory.DragCell = this;
        Debug.Log(12324124);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _uiViewInventory.DragCell = null;
        Debug.Log(5125125);
    }
    
}
