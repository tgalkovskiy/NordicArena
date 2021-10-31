using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int _id;
    private UIView _uiView;
    public InventoryEnum _InventoryEnum;
    
    public void Init(UIView view)
    {
        _uiView = view;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _uiView.DragCell = this;
        Debug.Log(12324124);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _uiView.DragCell = null;
        Debug.Log(5125125);
    }
    
}
