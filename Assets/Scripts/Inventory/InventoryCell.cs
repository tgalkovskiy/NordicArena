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
}
public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private int _id;
    public string _name;
    public string _description;
    public Image _imageCell;
    public Text _cellText;
    public GameObject _dragCellPrefab;
    private GameObject _dragCell;
    
    public void SetParameters(CellData cellData)
    {
        _id = cellData.Id; _name = cellData.name; _description = cellData.description;
        _cellText.text = _name;
        _cellText.gameObject.SetActive(true);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_name.IsNullOrEmpty())
        {
            _cellText.text = _description;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_name.IsNullOrEmpty())
        {
            _cellText.text = _name;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (_name.IsNullOrEmpty()) {
            return;
        }
         _dragCell = Instantiate(_dragCellPrefab);
         Text dragText = _dragCell.GetComponentInChildren<Text>();
         dragText.text = _name;
        _dragCell.transform.SetParent(gameObject.transform.parent.transform);
    }

    public void OnDrag(PointerEventData eventData) {
        if (_dragCell) {
            _dragCell.transform.position = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (_dragCell) {
            Destroy(_dragCell);
            SwapCellsFromPosition(eventData.position);
        }
    }

    private void SwapCellsFromPosition(Vector2 position) {
        InventoryCell hoveredCell = _dragCell.transform.parent.parent.parent.GetComponent<UIView>().GetCellByPosition(position);
        if (hoveredCell && hoveredCell._id != _id) {
            string tempName = _name;
            string tempDesc = _description;
            Image tempImg = _imageCell;

            _name = hoveredCell._name;
            _description = hoveredCell._description;
            _imageCell = hoveredCell._imageCell;
            _cellText.text = _name;

            hoveredCell._name = tempName;
            hoveredCell._description = tempDesc;
            hoveredCell._imageCell = tempImg;
            hoveredCell._cellText.text = tempName;

            if (hoveredCell._cellText.text.IsNullOrEmpty()) {
                hoveredCell._cellText.gameObject.SetActive(false);
            } else {
                hoveredCell._cellText.gameObject.SetActive(true);
            }

            if (_cellText.text.IsNullOrEmpty()) {
                _cellText.gameObject.SetActive(false);
            } else {
                _cellText.gameObject.SetActive(true);
            }
        }
    }
}
