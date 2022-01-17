using System.Collections;
using Cinemachine;
using UnityEngine;

public class PlayerView : View
{
    public UIViewInventory _viewInventory;
    public CinemachineVirtualCamera _cinemachine;
    public Camera mainCamera;
    public Camera miniMapCamera;
    public GameObject indicationPosition;
    
    
    private void Start()
    {
        _viewInventory.Init();
    }
    public void ShowUiInventory()
    {
        _viewInventory.ShowInventoryPanel();
    }

    public void SetDataCell(CellData cellData, GameObject gameObject)
    {
        if (_viewInventory.SetCell(cellData))
        {
            StartCoroutine(DestroyObj(gameObject));
        }
    }
    IEnumerator DestroyObj(GameObject gameObject)
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
