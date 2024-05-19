using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public List<GameObject> levels;
    public AnimationCurve speedCurve;
    public float duration = 2f;
    public int currLevel = 1;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("outPath") && currLevel == 1) // start level 1
        {
            //here should be animation of death badabi badaba
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (other.CompareTag("outPath"))
        {
            StartCoroutine(TransitionLevels(levels[currLevel - 2], levels[currLevel-1]));
            currLevel--;
        }
        else if (other.CompareTag("endLevel")) currLevel++;
    }



    public void setPosition(Vector3 pos)
    {
        //swith playerIndex
        transform.position = pos;
    }

    public void setRotation(Quaternion quat)
    {
        Matrix4x4 mat = Matrix4x4.Rotate(quat);
        transform.localRotation = quat;
    }


    IEnumerator TransitionLevels(GameObject prevLevel, GameObject currentLevel)
    {
        float startTime = Time.time;
        Vector3 currentLevelStartPos = currentLevel.transform.position;
        Vector3 nextLevelStartPos = prevLevel.transform.position;
        Vector3 currentLevelEndPos = currentLevelStartPos + new Vector3(0, 1000, 0);
        Vector3 nextLevelEndPos = nextLevelStartPos + new Vector3(0, 1000, 0);

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            float curveValue = speedCurve.Evaluate(t);
            currentLevel.transform.position = Vector3.Lerp(currentLevelStartPos, currentLevelEndPos, curveValue);
            prevLevel.transform.position = Vector3.Lerp(nextLevelStartPos, nextLevelEndPos, curveValue);
            yield return null;
        }

        currentLevel.transform.position = currentLevelEndPos;
        prevLevel.transform.position = nextLevelEndPos;
    }

}
