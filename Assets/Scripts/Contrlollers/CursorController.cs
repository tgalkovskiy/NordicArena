
using System;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public bool isChangeTypeCursor;
    public Texture2D defCursor;
    public Texture2D attackCursor;
    public Texture2D takeCursor;
    private Camera camera;
    private RaycastHit _hit;
    private Ray ray;
    
    private void Start()
    {
        camera = GetComponent<PlayerView>().mainCamera;
    }

    private void FixedUpdate()
    {
        if(!isChangeTypeCursor) return;
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out _hit, 100)) return;
        if (_hit.collider.gameObject.GetComponent<MonstersView>())
        {
            Cursor.SetCursor(attackCursor, Vector2.zero, CursorMode.Auto);
        }
        else if (_hit.collider.gameObject.GetComponent<DataObj>())
        {
            Cursor.SetCursor(takeCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(defCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
