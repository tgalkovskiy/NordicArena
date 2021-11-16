using System.Collections;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerView : View
{
    public UIViewInventory _viewInventory;
    public CinemachineVirtualCamera cinemachine;

    private void Awake()
    {
        Application.targetFrameRate = 90;
        _photonView = GetComponent<PhotonView>();
        _Presenter = new Presenter(this);
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
