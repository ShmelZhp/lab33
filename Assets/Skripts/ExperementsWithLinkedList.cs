using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace lab3
{
    public class ExperementsWithLinkedList: MonoBehaviour
    {
     [SerializeField] TMP_Dropdown dropdown;

     [SerializeField] Button startButton;
     [SerializeField] Slider slider;


     private void GenerateDataInt(int n,LinkedList<int> linkedList)
     {
         
         for (int i = 0; i < n; i++)
         {
             var temp = Random.value;
             linkedList.AddLast((int)temp);
         }
        
     }

     private void GenerateDataString(int n, LinkedList<string> linkedList)
     {

         for (int i = 0; i < n; i++)
         {
             var temp = Random.value;
             linkedList.AddLast(temp.ToString());
         }
     }

     private void MakeExperements(int index)
     {
         LinkedList<int> linkedList = new LinkedList<int>();
         LinkedList<string> linkedListString = new LinkedList<string>();
         GenerateDataInt((int)slider.value  ,linkedList);
         GenerateDataString((int)slider.value, linkedListString);
         switch (index)
         {
             case 0:
                 
                 LinkedListOperations.Reverse(linkedList);
                 break;
             case 1:
                 LinkedListOperations.MoveLastToFirst(linkedList);
                 break;
             case 2:
                 LinkedListOperations.MoveFirstToLast(linkedList);
                 break;
             case 3:
                 LinkedListOperations.CountDistinct(linkedList);
                 break;
             case 4:
                 LinkedListOperations.RemoveNonUnique(linkedList);
                 break;
             case 5:
                 break;
         }
     }



    }
}