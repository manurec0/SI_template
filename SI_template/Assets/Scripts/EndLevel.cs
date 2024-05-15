using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private bool player1IsEnd, player2IsEnd = false;

    // Update is called once per frame
    void Update()
    {
        if (player1IsEnd && player2IsEnd)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player1IsEnd = true;
        }
        
        if (other.CompareTag("player2"))
        {
            player2IsEnd = true;
        }
    }

}
