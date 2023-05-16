using UnityEngine;
using UnityEngine.UI;

public class SelectedExhibitInteractPopup : MonoBehaviour
{
    [SerializeField] private Image _interactPopup;

    private void OnEnable()
    {
        PlayerController.Instance.OnSelectedExhibitChanged += Player_OnSelectedExhibitChanged;
    }

    private void OnDisable()
    {
        PlayerController.Instance.OnSelectedExhibitChanged -= Player_OnSelectedExhibitChanged;
    }

    private void Player_OnSelectedExhibitChanged(object sender, PlayerController.OnSelectedExhibitChangedEventArgs e)
    {
        if (e._selectedExhibitEventArg != null)
        {
            _interactPopup.gameObject.SetActive(true);
        }
        else
        {
            _interactPopup.gameObject.SetActive(false);
        }
    }
}
