using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour
{
    public float moveDuration = 2f; // Duration of the animation
    public GameObject bg;

    private void OnEnable()
    {
        // Subscribe to the event with the direction parameter
        LevelChange.OnMoveObject += MoveObject;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        LevelChange.OnMoveObject -= MoveObject;
    }

    // Method to handle the movement with direction and an additional GameObject
    private void MoveObject(bool moveUp, Transform gameObj)
    {
        
        Vector3 targetPositionSelf = transform.position + new Vector3(0, moveUp ? 1000 : -1000, 0);
        Vector3 targetPositionObj = gameObj.position + new Vector3(0, moveUp ? 1000 : -1000, 0);
        Vector3 targetPositionBg = bg.transform.position + new Vector3(0, moveUp ? 100 : -100, 0);

        StartCoroutine(AnimateMovement(transform, targetPositionSelf, gameObj, targetPositionObj, bg.transform, targetPositionBg, moveDuration));
    }

    // Coroutine to animate the movement of the objects and bg
    private IEnumerator AnimateMovement(Transform startTransform, Vector3 endPositionSelf, Transform gameObjTransform, Vector3 endPositionObj, Transform bgTransform, Vector3 endPositionBg, float duration)
    {
        float elapsedTime = 0;

        Vector3 startPositionSelf = startTransform.position;
        Vector3 startPositionObj = gameObjTransform.position;
        Vector3 startPositionBg = bgTransform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            startTransform.position = Vector3.Lerp(startPositionSelf, endPositionSelf, t);
            gameObjTransform.position = Vector3.Lerp(startPositionObj, endPositionObj, t);
            bgTransform.position = Vector3.Lerp(startPositionBg, endPositionBg, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        startTransform.position = endPositionSelf;
        gameObjTransform.position = endPositionObj;
        bgTransform.position = endPositionBg;
    }
}
