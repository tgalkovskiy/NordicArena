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
    public ItemCell DragCell;
    [SerializeField] private GameObject _inventoryPanel = default;
    public Transform _cellContainer;
    [SerializeField] private List<ItemCell> _itemCells = new List<ItemCell>();
    private List<InventoryCell> _cellsInventory = new List<InventoryCell>();
    private bool isActivInventory = false;
    
    public void ShowInventoryPanel()
    {
        isActivInventory = !isActivInventory;
        _inventoryPanel.SetActive(isActivInventory);
    }
    public void Init()
    {
        foreach (var t in _itemCells)
        {
            t.Init(this);
        }
    }
    public bool SetCell(CellData cellData)
    {
        _cellsInventory.Add(CreateCellInventory(cellData));
        return true;
    }
    public void DeleteInventoryCell()
    {
        Destroy(_cellsInventory[_cellsInventory.Count-1].gameObject);
        _cellsInventory.RemoveAt(_cellsInventory.Count-1);
    }
    private InventoryCell CreateCellInventory(CellData cellData)
    {
        var cell = Instantiate(_cellPrefabs, _cellContainer);
        cell.SetParameters(cellData, this);
        return cell;
    }
    
    
}
