using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _textMeshPro;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Button _welcomeButton;
    [SerializeField] private TMP_Text _welcomeText;
    private float timer = 0f;
    private bool isFadingOut = false;

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

            FadeBlackScreenBackgroundAlpha(alpha);
            FadeTextOnBackgroundAlpha(alpha);
            FadeWelcomeButtonTextAlpha(alpha);
            FadeWelcomeButton(alpha);

            if (timer >= _duration)
            {
                isFadingOut = false;
                timer = 0f;
                gameObject.SetActive(false);
            }
        }
    }

    private void FadeWelcomeButton(float alpha)
    {
        if (_welcomeButton != null)
        {
            Color buttonColor = _welcomeButton.colors.normalColor;
            buttonColor.a = alpha;
        }
    }

    private void FadeWelcomeButtonTextAlpha(float alpha)
    {
        if (_welcomeText != null)
        {
            Color textColor = _welcomeText.color;
            textColor.a = alpha;
            _welcomeText.color = textColor;
        }
    }

    private void FadeTextOnBackgroundAlpha(float alpha)
    {
        if (_textMeshPro != null)
        {
            Color textColor = _textMeshPro.color;
            textColor.a = alpha;
            _textMeshPro.color = textColor;
        }
    }

    private void FadeBlackScreenBackgroundAlpha(float alpha)
    {
        if (_backgroundImage != null)
        {
            Color imageColor = _backgroundImage.color;
            imageColor.a = alpha;
            _backgroundImage.color = imageColor;
        }
    }

    public void StartFadeOut()
    {
        isFadingOut = true;
    }
}
