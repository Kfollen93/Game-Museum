using Cinemachine;
using UnityEngine;

public class CameraControlLock : MonoBehaviour
{
    public static CameraControlLock Instance { get; private set; }
    [SerializeField] private CinemachineVirtualCamera _vCam;
    private CinemachineInputProvider _provider;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _provider = _vCam.GetComponent<CinemachineInputProvider>();
    }

    // Also called by onclick from the Welcome button.
    public void EnableCinemachineCameraInput()
    {
        _provider.enabled = true;
    }

    public void DisableCinemachineCameraInput()
    {
        _provider.enabled = false;
    }
}
