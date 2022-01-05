
using Cinemachine;
using UnityEngine;

public class CameraControllers
{
    private CinemachineOrbitalTransposer cinemachineOrbitalTransposer;
    private CinemachineVirtualCamera cinemachineVirtual;
    private CinemachineFollowZoom cinemachineFollow;
    private  Camera miniMapCamera;
    public CameraControllers(CinemachineVirtualCamera cinemachineVirtual, Camera miniMapCamera)
    {
        this.cinemachineVirtual = cinemachineVirtual;
        cinemachineOrbitalTransposer = this.cinemachineVirtual.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        cinemachineFollow = this.cinemachineVirtual.gameObject.GetComponent<CinemachineFollowZoom>();
        this.miniMapCamera = miniMapCamera;
    }

    public void TurnCameraRight()
    {
        cinemachineOrbitalTransposer.m_XAxis.Value += 1;
        miniMapCamera.transform.rotation *= Quaternion.Euler(0,0,1); 
    }
    public void TurnCameraLeft()
    {
        cinemachineOrbitalTransposer.m_XAxis.Value -= 1;
        miniMapCamera.transform.rotation *= Quaternion.Euler(0,0,-1); 
    }

    public void ReturnCameraAngleDefault()
    {
        cinemachineOrbitalTransposer.m_XAxis.Value = 0;
        cinemachineFollow.m_Width = 20;
    }
    public void ZoomCamera(float zoom)
    {
        cinemachineFollow.m_Width -= zoom*3;
    }
}
