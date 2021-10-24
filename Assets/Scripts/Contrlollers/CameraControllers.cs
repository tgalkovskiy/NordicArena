using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControllers
{
    private CinemachineOrbitalTransposer _cinemachineOrbitalTransposer;
    private CinemachineVirtualCamera _cinemachineVirtual;
    private CinemachineFollowZoom _cinemachineFollow;
    public CameraControllers(CinemachineVirtualCamera cinemachineVirtual)
    {
        _cinemachineVirtual = cinemachineVirtual;
        _cinemachineOrbitalTransposer = _cinemachineVirtual.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        _cinemachineFollow = _cinemachineVirtual.gameObject.GetComponent<CinemachineFollowZoom>();
    }

    public void TurnCameraRight()
    {
        _cinemachineOrbitalTransposer.m_XAxis.Value += 1;
    }
    public void TurnCameraLeft()
    {
        _cinemachineOrbitalTransposer.m_XAxis.Value -= 1;
    }

    public void ReturnCameraAngleDefault()
    {
        _cinemachineOrbitalTransposer.m_XAxis.Value = 0;
        _cinemachineFollow.m_Width = 20;
    }
    public void ZoomCamera(float zoom)
    {
        _cinemachineFollow.m_Width -= zoom*3;
    }
}
