using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCrankController : MonoBehaviour
{
    public GameObject target;
    public Vector3 moveTargetDir;
    public GameObject player;
    public Vector3 angularVelocity;
    private float angle;
    private Rigidbody rb;
    private IPlateAction actionInterface;
    bool playerOnWheel;
    // Start is called before the first frame update
    void Start()
    {
        angle = transform.rotation.x;
        playerOnWheel = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        angularVelocity = rb.velocity;
        if (playerOnWheel)
        {
            //transform.LookAt(player.transform);
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
        MoveTarget(target, rb.angularVelocity);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("Player2"))
            playerOnWheel = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
            playerOnWheel = false;
    }

    private void MoveTarget(GameObject platform, Vector3 dir)
    {
        platform.transform.position += dir;
    }
}