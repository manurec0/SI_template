using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public MonoBehaviour actionTarget; 
    private IPlateAction actionInterface;
    private bool isPlayerOnButton;

    void Start()
    {
        actionInterface = actionTarget as IPlateAction;
        isPlayerOnButton = false;



    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerOnButton = !isPlayerOnButton;
        actionInterface.ExecuteAction(isPlayerOnButton);
    }

    //private void OnTriggerExit(Collider other)
    //{
     
    //    actionInterface.ExecuteAction(false);
    //}
}
