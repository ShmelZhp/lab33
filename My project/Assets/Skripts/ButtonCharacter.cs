using System;
using UnityEngine;
using UnityEngine.UI;

namespace lab3
{
    public class ButtonCharacter:MonoBehaviour
    {
        [SerializeField] private GameObject CurrentScreen;
        [SerializeField] private GameObject NewScreen;

        public void init()
        {
            gameObject.GetComponent<Button>().onClick.AddListener( () => ChangeScreen.Change(CurrentScreen,NewScreen));
            
        }
    }
}