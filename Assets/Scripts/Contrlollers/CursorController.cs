
using System;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]public Texture2D _defCursor;
    [SerializeField]public Texture2D _attackCursor;
    [SerializeField]public Texture2D _takeCursor;
    private Camera camera;
    private RaycastHit _hit;

    private void Awake()
    {
        camera = GetComponent<InputController>().mainCamera;
    }

    private void FixedUpdate()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, 100))
        {
            if (_hit.collider.gameObject.GetComponent<MonstersView>())
            {
                Cursor.SetCursor(_attackCursor, Vector2.zero, CursorMode.Auto);
            }
            else if (_hit.collider.gameObject.GetComponent<DataObj>())
            {
                Cursor.SetCursor(_takeCursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(_defCursor, Vector2.zero, CursorMode.Auto);
            }
        }
    }
}
