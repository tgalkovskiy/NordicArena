
using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    public Camera gameCamera;

    private void Awake()
    {
        //gameCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        transform.LookAt(gameCamera.transform.position);
    }
}
