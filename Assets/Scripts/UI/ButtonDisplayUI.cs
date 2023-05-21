using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplayUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _delayDisplayAmount;
    private float timer = 0;

    private void Start()
    {
        _button.gameObject.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _delayDisplayAmount) _button.gameObject.SetActive(true);
    }
}
