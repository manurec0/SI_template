using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public MonoBehaviour actionTarget;
    public GameObject lightPlane;
    public AudioSource buttonOn;
    //public AudioSource buttonOff;
    private IPlateAction actionInterface;
    private bool buttonState;

    void Start()
    {
        actionInterface = actionTarget as IPlateAction;
        buttonState = false;
        lightPlane.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        buttonState = !buttonState;
        actionInterface.ExecuteAction(buttonState);
        lightPlane.SetActive(buttonState);
        buttonOn.Play();
    }

    //private void OnTriggerExit(Collider other)
    //{
     
    //    actionInterface.ExecuteAction(false);
    //}
}
