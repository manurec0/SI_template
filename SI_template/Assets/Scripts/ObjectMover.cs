using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour
{
    public float moveDuration = 2f; // Duration of the animation

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

    // Method to handle the movement with direction
    private void MoveObject(bool moveUp)
    {
        Vector3 targetPosition = transform.position + new Vector3(0, moveUp ? 1000 : -1000, 0);
        StartCoroutine(AnimateMovement(transform.position, targetPosition, moveDuration));
    }

    // Coroutine to animate the movement
    private IEnumerator AnimateMovement(Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }
}
