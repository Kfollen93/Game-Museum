using System;
using TMPro;
using UnityEngine;

public class GraphicsSetting : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    private void Awake() => _dropdown = GetComponent<TMP_Dropdown>();

    private void Start()
    {
        _dropdown.value = (int)GraphicsQuality.Medium;
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    } 

    private void OnDropdownValueChanged(int value)
    {
        string selectedOption = _dropdown.options[value].text;
        selectedOption = ParseRayTracingValues(selectedOption);

        if (Enum.TryParse(selectedOption, out GraphicsQuality graphicsQuality))
        {
            switch (graphicsQuality)
            {
                case GraphicsQuality.Low:
                    QualitySettings.SetQualityLevel(4);
                    break;
                case GraphicsQuality.Medium:
                    QualitySettings.SetQualityLevel(3);
                    break;
                case GraphicsQuality.High:
                    QualitySettings.SetQualityLevel(2);
                    break;
                case GraphicsQuality.RtLow:
                    QualitySettings.SetQualityLevel(1);
                    break;
                case GraphicsQuality.RtMax:
                    QualitySettings.SetQualityLevel(0);
                    break;
                default:
                    QualitySettings.SetQualityLevel(3);
                    break;
            }
        }
    }

    private static string ParseRayTracingValues(string selectedOption)
    {
        if (selectedOption == "RT Low") selectedOption = "RtLow";
        if (selectedOption == "RT Max") selectedOption = "RtMax";
        return selectedOption;
    }

    public enum GraphicsQuality
    {
        Low,
        Medium,
        High,
        RtLow,
        RtMax
    }
}
