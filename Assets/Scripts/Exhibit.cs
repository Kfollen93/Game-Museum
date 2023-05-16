using UnityEngine;

public class Exhibit : MonoBehaviour
{
    [SerializeField] private ExhibitSO _exhibitSO;

    public ExhibitSO GetExhibitSO() => _exhibitSO;
}
