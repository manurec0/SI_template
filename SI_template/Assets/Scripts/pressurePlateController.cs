
using UnityEngine;

public class pressurePlateController : MonoBehaviour
{
    public MonoBehaviour actionTarget; 

    private IPlateAction actionInterface;
        public GameObject lightPlane;
    public AudioSource plateOn;
    public AudioSource plateOff;

    void Start()
    {
        actionInterface = actionTarget as IPlateAction;


    }

    private void OnTriggerEnter(Collider other)
    {
            actionInterface.ExecuteAction(true);
            lightPlane.SetActive(true);
            plateOn.Play();
    }

    private void OnTriggerExit(Collider other)
    {
     
            actionInterface.ExecuteAction(false);
            plateOff.Play();
            lightPlane.SetActive(false);
    }
}
