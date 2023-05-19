using Cinemachine;
using UnityEngine;

public class CameraControlLock : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCam;
    // Connected to the welcome button.
    public void EnableCinemachineCameraInput()
    {
        var cmInput = _vCam.GetComponent<CinemachineInputProvider>();
        cmInput.enabled = true;
    }

}
