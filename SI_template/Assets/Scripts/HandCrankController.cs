using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCrankController : MonoBehaviour
{
    public GameObject target;
    public Vector3 moveTargetDir;
    public GameObject player;
    public Vector3 angularVelocity;
    private Rigidbody rb;
    public float angle;
    private float angleStart;
    public float angleTiles = -90;

    private Vector3 tile1Other;
    private Vector3 tile2Other;
    private Transform tile1;
    private Transform tile2;

    private IPlateAction actionInterface;
    bool playerOnWheel;
    // Start is called before the first frame update

    void Start()
    {
        angle = transform.rotation.x;
        angleStart = transform.rotation.x;
        tile1 = target.transform.GetChild(0);
        tile2 = target.transform.GetChild(1);
        tile1Other = new Vector3(tile1.rotation.x, tile1.rotation.y, tile1.rotation.w);
        tile2Other = new Vector3(tile2.rotation.x, tile2.rotation.y, tile2.rotation.w);

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
            if (playerOnWheel && angleTiles != 0)
            {
                transform.LookAt(player.transform);
                //aqui posa la traslacio al angle z nomes els dos objectes estan igual llavors hauries de posar el mateix valor no es en plan que ho hagis de fer al reves
                angle = transform.rotation.x;

                angleTiles += (angle) / 2;
                Debug.Log($"{tile1.rotation}");
                tile1.rotation.Set(tile1Other.x, tile1Other.y, angleTiles, tile1Other.z);
                tile2.rotation.Set(tile2Other.x, tile2Other.y, angleTiles, tile2Other.z);

                //tile1.LookAt(transform) si gires x graus el transform que la tile giri x/2 o algo aixi 
            }
            if (angleTiles == 0)
            {
                target.GetComponent<BoxCollider>().enabled = false;
            }
            MoveTarget(target, rb.angularVelocity);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
                playerOnWheel = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
                playerOnWheel = false;
        }

        void MoveTarget(GameObject platform, Vector3 dir)
        {
            platform.transform.position += dir;
        }

    }
}