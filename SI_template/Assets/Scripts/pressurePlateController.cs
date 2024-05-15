using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlateController : MonoBehaviour
{
    public GameObject lightPlane;
    public AudioSource plateOn;
    public AudioSource plateOff;
    public bool isOnButton;
    // Start is called before the first frame update
    void Start()
    {
        isOnButton = false;
        lightPlane.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        lightPlane.SetActive(true);
        plateOn.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        plateOff.Play();
        lightPlane.SetActive(false);
    }
}
