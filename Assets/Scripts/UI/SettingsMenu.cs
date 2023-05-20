using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private bool _toggleMenu;
    [SerializeField] private GameObject _settingsGO;

    private void Awake()
    {
        _settingsGO.SetActive(false);
    }

    private void Start()
    {
        GameInput.Instance.OnSettingsAction += GameInput_OnSettingsAction;
    }

    private void OnDisable()
    {
        GameInput.Instance.OnSettingsAction -= GameInput_OnSettingsAction;
    }

    private void GameInput_OnSettingsAction(object sender, System.EventArgs e)
    {
        _toggleMenu = !_toggleMenu;
        PauseCamera();
        DisplayMenu(_toggleMenu);
    }

    private void DisplayMenu(bool _toggleMenu) => _settingsGO.SetActive(_toggleMenu);

    private void PauseCamera()
    {
        if (_toggleMenu)
        {
            CameraControlLock.Instance.DisableCinemachineCameraInput();
        }
        else
        {
            CameraControlLock.Instance.EnableCinemachineCameraInput();
        }
    }
}
