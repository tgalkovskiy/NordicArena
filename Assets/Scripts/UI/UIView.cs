using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WebSocketSharp;

public sealed class UIView : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel = default;
    [SerializeField] private List<InventoryCell> _cells = new List<InventoryCell>();
    private bool isActivInventory = false;

    public void ShowInventoryPanel()
    {
        isActivInventory = !isActivInventory;
        _inventoryPanel.SetActive(isActivInventory);
    }
    public bool SetCell(CellData cellData)
    {
        Debug.Log("find sell");
        foreach (var cell in _cells)
        {
            if(cell._name.IsNullOrEmpty())
            {
                Debug.Log(cell.name);
                cell.SetParameters(cellData);
                return true;
            }
        }
        return false;
    }
}
