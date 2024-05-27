using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandCrankController : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;
    public GameObject player;

    public float angle;

    public Quaternion angle1;
    public Quaternion angle2;

    public float debugRotation1;
    public float debugRotation2;

    private Rigidbody rb;
    private bool locked;

    bool playerOnWheel;
    // Start is called before the first frame update
    void Start()
    {
        angle = transform.rotation.eulerAngles.y;
        playerOnWheel = false;
        rb = GetComponent<Rigidbody>();
        locked = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        angle = transform.rotation.eulerAngles.y;
        angle1 = Quaternion.Euler(0,0,angle);
        angle2 = Quaternion.Euler(0, 180, angle);
        debugRotation1 = target1.transform.rotation.z;
        debugRotation2 = target2.transform.rotation.z;
        if (playerOnWheel)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            if ((angle < 360f && angle > 180f) && (!locked))
            {
                target1.transform.rotation = angle1;
                target2.transform.rotation = angle2;
            }

            else
                locked = true;
        }
        if (locked)
            target1.GetComponentInParent<BoxCollider>().enabled = false;

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
            playerOnWheel = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
            playerOnWheel = false;
    }

    private void RotateTarget(GameObject platform, float angle)
    {
        platform.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}