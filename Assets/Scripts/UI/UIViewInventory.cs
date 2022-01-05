using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WebSocketSharp;

public sealed class UIViewInventory : MonoBehaviour
{
    public InventoryCell _cellPrefabs;
    public GameObject _spawnObj;
    public InventoryCell DragCell;
    [SerializeField] private GameObject _inventoryPanel = default;
    public Transform _cellContainer;
    [SerializeField] private List<InventoryCell> itemCells = new List<InventoryCell>();
    [SerializeField] private List<InventoryCell> cellsInventory = new List<InventoryCell>();
    private bool isActivInventory = false;
    
    public void ShowInventoryPanel()
    {
        isActivInventory = !isActivInventory;
        _inventoryPanel.SetActive(isActivInventory);
    }
    public void Init()
    {
        foreach (var t in cellsInventory)
        {
            t.Init(this);
        }
        foreach (var t in itemCells)
        {
            t.Init(this);
        }
    }
    public bool SetCell(CellData cellData)
    {
        foreach (var t in cellsInventory.Where(t => t.nameItem.IsNullOrEmpty()))
        {
            t.SetDataCell(cellData);
            break;
        }
        //cellsInventory.Add(CreateCellInventory(cellData));
        return true;
    }
    /*public void DeleteInventoryCell()
    {
        Destroy(cellsInventory[cellsInventory.Count-1].gameObject);
        cellsInventory.RemoveAt(cellsInventory.Count-1);
    }
    private InventoryCell CreateCellInventory(CellData cellData)
    {
        var cell = Instantiate(_cellPrefabs, _cellContainer);
        cell.SetParameters(cellData, this);
        return cell;
    }*/
    
    
}
