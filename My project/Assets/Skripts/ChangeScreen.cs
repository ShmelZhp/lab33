using UnityEngine;

namespace lab3
{
    public  class ChangeScreen : MonoBehaviour
    {

        public static void Change(GameObject CurrentScreen, GameObject NewScreen)
        {
            CurrentScreen.SetActive(false);
            NewScreen.SetActive(true);
        }
        
    }
}