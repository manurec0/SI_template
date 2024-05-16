using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public MonoBehaviour actionTarget; 
    private IPlateAction actionInterface;

    void Start()
    {
        actionInterface = actionTarget as IPlateAction;

        
    }

    private void OnTriggerEnter(Collider other)
    {
            actionInterface.ExecuteAction(true);
    }

    private void OnTriggerExit(Collider other)
    {
     
            actionInterface.ExecuteAction(false);
    }
}
