using UnityEngine;
using Unity.Cinemachine;   // <-- WAJIB untuk Cinemachine 3

public class CameraSwitch : MonoBehaviour
{
    public CinemachineCamera normalCam;
    public CinemachineCamera closeUpCam;

    public void ZoomIn()
    {
        normalCam.Priority = 0;
        closeUpCam.Priority = 10;
    }

    public void ZoomOut()
    {
        normalCam.Priority = 10;
        closeUpCam.Priority = 0;
    }
}