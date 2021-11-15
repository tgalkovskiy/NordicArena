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
    public ItemCell DragCell;
    [SerializeField] private GameObject _inventoryPanel = default;
    [SerializeField] private Transform _inventoryСontainer = default;
    [SerializeField] private Transform _cellContainer;
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
        for (int i = 0; i < _itemCells.Count; i++)
        {
            _itemCells[i].Init(this);
        }
        CreateCellInventory(44);
    }
    public bool SetCell(CellData cellData)
    {
        foreach (var cell in _cellsInventory)
        {
            if(cell._name.IsNullOrEmpty())
            {
                cell.SetParameters(cellData);
                return true;
            }
        }
        return false;
    }
    public void DeleteInventoryCell()
    {
        Destroy(_cellsInventory[_cellsInventory.Count-1].gameObject);
        _cellsInventory.RemoveAt(_cellsInventory.Count-1);
    }
    public void CreateCellInventory(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _cellsInventory.Add(Instantiate(_cellPrefabs, _cellContainer));
            _cellsInventory[_cellsInventory.Count-1].Init(_inventoryСontainer, _cellContainer, this);
            _cellsInventory[_cellsInventory.Count - 1].name = (_cellsInventory.Count - 1).ToString();
            _cellsInventory[_cellsInventory.Count-1].SetParameters(new CellData(){ Id =_cellsInventory.Count-1, name = string.Empty, description = string.Empty});
        }
    }
    
    
}
