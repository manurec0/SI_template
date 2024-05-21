using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{

    public List<GameObject> levelsList;
    public AnimationCurve speedCurve;
    public float duration = 2f;
    public TextMeshProUGUI levelCounterObj;
    public int counter;

    private void Start()
    {
        counter = 0;
        levelCounterObj.text = counter.ToString();
    }

    private void Update()
    {
        //this might cause issues as im updating the value of level counter with currLevel and in TRANSITION LEVEL im doing it the other way around
        // but we need it as if its the other player that falls this player wouldnt know it and say they are still in the same level 
        //int.TryParse(levelCounterObj.text, out counter);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("outPath") && counter == 0) 
        {
            //here should be animation of death badabi badaba
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        }
        else if (other.CompareTag("outPath"))
        {
            StartCoroutine(TransitionLevels(levelsList[counter - 1], levelsList[counter]));
        }
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
        counter--;
        levelCounterObj.text = counter.ToString();
        LevelChange.LevelUp(counter);
        float startTime = Time.time;
        Vector3 currentLevelStartPos = currentLevel.transform.position;
        Vector3 nextLevelStartPos = prevLevel.transform.position;
        Vector3 currentLevelEndPos = currentLevelStartPos + new Vector3(0, 1000, 0);
        Vector3 nextLevelEndPos = nextLevelStartPos + new Vector3(0, 1000, 0);

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            float curveValue = speedCurve.Evaluate(t);
            currentLevel.transform.position = Vector3.Lerp(currentLevelEndPos, currentLevelStartPos, curveValue);
            prevLevel.transform.position = Vector3.Lerp(nextLevelEndPos, nextLevelStartPos,  curveValue);
            yield return null;
        }

        currentLevel.transform.position = currentLevelEndPos;
        prevLevel.transform.position = nextLevelEndPos;
    }



    void OnEnable()
    {
        LevelChange.OnLevelUp += UpdateLocalCounter;
    }

    void OnDisable()
    {
        LevelChange.OnLevelUp -= UpdateLocalCounter;
    }

    private void UpdateLocalCounter(int newLevel)
    {
        counter = newLevel;

    }
}
