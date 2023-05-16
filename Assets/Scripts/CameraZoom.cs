using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCam;
    private const float ZOOM_DEFAULT = 60f;
    private const float ZOOM_IN = 52f;
    [SerializeField] private float _zoomSpeed = 5f;
    private bool _isZooming = false;
    private Coroutine _zoomCoroutine;

    private void Start()
    {
        GameInput.Instance.OnZoomAction += GameInput_OnZoomAction;
        GameInput.Instance.OnZoomCanceled += GameInput_OnZoomCanceled;
    }

    private void OnDisable()
    {
        GameInput.Instance.OnZoomAction -= GameInput_OnZoomAction;
        GameInput.Instance.OnZoomCanceled -= GameInput_OnZoomCanceled;
    }

    private void GameInput_OnZoomAction(object sender, System.EventArgs e)
    {
        if (!_isZooming)
        {
            _isZooming = true;
            _zoomCoroutine = StartCoroutine(ZoomCoroutine(ZOOM_IN));
        }
    }

    private void GameInput_OnZoomCanceled(object sender, System.EventArgs e)
    {
        if (_isZooming)
        {
            _isZooming = false;
            StopCoroutine(_zoomCoroutine);
            _zoomCoroutine = StartCoroutine(ZoomCoroutine(ZOOM_DEFAULT));
        }
    }

    private IEnumerator ZoomCoroutine(float zoomFOV)
    {
        float threshold = 0.01f;
        while (Mathf.Abs(_vCam.m_Lens.FieldOfView - zoomFOV) > threshold)
        {
            float currentFOV = Mathf.Lerp(_vCam.m_Lens.FieldOfView, zoomFOV, Time.unscaledDeltaTime * _zoomSpeed);
            _vCam.m_Lens.FieldOfView = currentFOV;
            yield return null;
        }
    }
}
