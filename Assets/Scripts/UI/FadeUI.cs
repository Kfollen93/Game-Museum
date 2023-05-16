using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private float _duration = 1f;

    private float timer = 0f;
    private bool isFadingOut = false;

    void Start()
    {
        if (_backgroundImage != null)
        {
            Color imageColor = _backgroundImage.color;
            imageColor.a = 1f;
            _backgroundImage.color = imageColor;
        }
        if (_textMeshPro != null)
        {
            Color textColor = _textMeshPro.color;
            textColor.a = 1f;
            _textMeshPro.color = textColor;
        }
    }

    void Update()
    {
        RunUIFade();
    }

    private void RunUIFade()
    {
        if (isFadingOut)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / _duration);
            if (_backgroundImage != null)
            {
                Color imageColor = _backgroundImage.color;
                imageColor.a = alpha;
                _backgroundImage.color = imageColor;
            }
            if (_textMeshPro != null)
            {
                Color textColor = _textMeshPro.color;
                textColor.a = alpha;
                _textMeshPro.color = textColor;
            }
            if (timer >= _duration)
            {
                isFadingOut = false;
                timer = 0f;
                gameObject.SetActive(false);
            }
        }
    }

    public void StartFadeOut()
    {
        isFadingOut = true;
    }
}
