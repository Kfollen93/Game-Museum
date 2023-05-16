using TMPro;
using UnityEngine;

public class ExhibitInformationUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameOfConsole;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _manufacturer;
    [SerializeField] private TMP_Text _releaseDate;
    [SerializeField] private TMP_Text _price;

    private void Start()
    {
        gameObject.SetActive(false);
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        PlayerController.Instance.OnSelectedExhibitChanged += Player_OnSelectedExhibitChanged;
    }

    private void Player_OnSelectedExhibitChanged(object sender, PlayerController.OnSelectedExhibitChangedEventArgs e)
    {
        if (e._selectedExhibitEventArg != null)
        {
            SetExhibitInformation(e);
        }
        else
        {
            HideExhibitUI();
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (gameObject.activeSelf)
        {
            HideExhibitUI();
        }
        else
        {
            DisplayExhibitUI();
        }
    }

    private void SetExhibitInformation(PlayerController.OnSelectedExhibitChangedEventArgs e)
    {
        ExhibitSO exhibitSO = e._selectedExhibitEventArg.GetExhibitSO();
        _nameOfConsole.text = exhibitSO.name;
        _description.text = exhibitSO.Description;
        _manufacturer.text = exhibitSO.Manufacturer;
        _releaseDate.text = exhibitSO.DateReleased;
        _price.text = exhibitSO.ReleasePrice;
    }

    private void DisplayExhibitUI() => gameObject.SetActive(true);

    private void HideExhibitUI() => gameObject.SetActive(false);
}
