using UnityEngine;
using TMPro;

public class Typewriter : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMesh;
    [SerializeField] private float _delayBetweenChars = 0.1f;
    [SerializeField] private string _inputText;
    private float timer = 0f;
    private int index = 0;

    private void Start()
    {
        _textMesh.text = "";
    }

    private void Update()
    {
        RunTypewriterEffect();
    }

    private void RunTypewriterEffect()
    {
        if (index < _inputText.Length)
        {
            timer += Time.deltaTime;
            if (timer >= _delayBetweenChars)
            {
                char c = _inputText[index];
                if (c == '\\')
                {
                    index++;
                    if (index < _inputText.Length)
                    {
                        c = _inputText[index];
                        if (c == 'n')
                        {
                            _textMesh.text += '\n';
                        }
                        else
                        {
                            _textMesh.text += '\\';
                            _textMesh.text += c;
                        }
                    }
                }
                else
                {
                    _textMesh.text += c;
                }
                index++;
                timer = 0;
            }
        }
    }
}
