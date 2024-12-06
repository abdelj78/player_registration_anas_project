// Code for changing the input field when pressing tab or shift+tab
// The exact flow between each UI element is set on the Unity Editor (navigation settings)
// Abdeljabbar Rebani 12/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class changeInput : MonoBehaviour
{
    EventSystem system;
    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {   //if tab is pressed and shift is pressed, go to the previous input field
        if (Input.GetKeyDown(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null){
                previous.Select();   
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab)) //if tab is pressed, go to the next input field
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null){
                next.Select();

            }
        }
    }   
}
