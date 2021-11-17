using System.Collections;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerView : View
{
    public UIViewInventory _viewInventory;
    public CinemachineVirtualCamera _cinemachine;

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
        yield return new WaitForSeconds(1);
        PhotonNetwork.Destroy(gameObject);
    }
}
