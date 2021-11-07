using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]public Texture2D _defCursor;
    [SerializeField]public Texture2D _attackCursor;
    [SerializeField]public Texture2D _takeCursor;
    private RaycastHit _hit;
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit, 100))
        {
            if (_hit.collider.gameObject.GetComponent<Enemy>())
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
