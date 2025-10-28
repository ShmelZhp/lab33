using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
     private Slider slider;
     [SerializeField] TextMeshProUGUI textMeshProUGUI;

     private void Awake()
     {
         slider = GetComponent<Slider>();
         slider.onValueChanged.AddListener((value) => {
             textMeshProUGUI.text = slider.value.ToString();
         });
     }
}
