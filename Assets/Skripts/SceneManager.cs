using System;
using System.Collections.Generic;
using lab3;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    [SerializeField] private Parser parser;
    [SerializeField] private StackExperements stackExperements;
    [SerializeField] private ChartManager chartManager;
    [SerializeField] private PostfixCalculator postfixCalculator;
    [SerializeField] private queParser queParser;
    
    [SerializeField] private GameObject InputStackScreen;
    [SerializeField] private GameObject stackChartsScreen;
    [SerializeField] private List<ButtonCharacter>  buttons;
    
    private void Awake()
    {
        parser.Init();
        chartManager.Init();
            postfixCalculator.Init();
            queParser.Init();
        foreach (ButtonCharacter button in buttons)
        {
            button.init();
        }
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    }

