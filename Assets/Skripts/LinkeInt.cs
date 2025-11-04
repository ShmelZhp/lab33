using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LinkedInt : MonoBehaviour
{
    [Header("Sliders")]
    public Slider firstSlider;
    public Slider secondSlider;
    
    [Header("Settings")]
    public int minDivision = 2;
    
   
    private bool isUpdating = false;

    void Awake()
    {
        // Устанавливаем целые числа
        firstSlider.wholeNumbers = true;
        secondSlider.wholeNumbers = true;
        
        firstSlider.onValueChanged.AddListener(OnFirstSliderChanged);
        secondSlider.onValueChanged.AddListener(OnSecondSliderChanged);
        
        
        
    }

    private void OnFirstSliderChanged(float value)
    {
        
    }

    private void OnSecondSliderChanged(float value)
    {
        if (isUpdating) return;
        
        isUpdating = true;
        int secondValue = (int)value;
        
        if ((int)firstSlider.value / secondValue < minDivision)
        {
            int minFirstValue = secondValue * minDivision;
            
            if ((int)firstSlider.value < minFirstValue)
            {
                firstSlider.value = minFirstValue;
            }
        }
        
       
        isUpdating = false;
    }

    

   
    }
