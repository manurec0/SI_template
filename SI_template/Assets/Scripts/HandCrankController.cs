using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCrankController : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    private float angle;
    private IPlateAction actionInterface;
    bool playerOnWheel;
    // Start is called before the first frame update
    void Start()
    {
        angle = transform.rotation.x;
        playerOnWheel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnWheel)
        {
            transform.LookAt(player.transform);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        playerOnWheel = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerOnWheel = false;
    }
}