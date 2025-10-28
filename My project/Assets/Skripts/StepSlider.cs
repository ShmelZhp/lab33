using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StepSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI displayText; // Для отображения значения

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        UpdateDisplay();
    }

    private void OnSliderValueChanged(float value)
    {
        // Округляем значение до ближайшего кратного 10
        int steppedValue = Mathf.RoundToInt(value / 10) * 10;
        
        // Если значение изменилось, обновляем слайдер
        if (steppedValue != slider.value)
        {
            slider.value = steppedValue;
        }
        
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
            displayText.text = slider.value.ToString();
    }
}