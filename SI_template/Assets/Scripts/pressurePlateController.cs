using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlateController : MonoBehaviour
{
    public GameObject lightPlane;
    public GameObject linkedTile;
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
        if (isOnButton)
        {
            lightPlane.SetActive(true);
        }
        else
            lightPlane.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        isOnButton = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOnButton = false;
    }
}
