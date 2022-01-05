using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIViewInventory uiViewInventory;
    public InventoryEnum inventoryEnum;
    
    public void Init(UIViewInventory viewInventory)
    {
        //uiViewInventory = viewInventory;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //uiViewInventory.DragCell = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //uiViewInventory.DragCell = null;
    }
    
}
