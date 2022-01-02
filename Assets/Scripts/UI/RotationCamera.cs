
using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    private Camera gameCamera;

    private void Awake()
    {
        gameCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        transform.LookAt(gameCamera.transform.position);
    }
}
