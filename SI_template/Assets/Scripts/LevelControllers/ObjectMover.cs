using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour
{
    public float moveDuration = 2f;
    public GameObject bg;
    public GameObject endBg;
    public GameObject fog;

    private void OnEnable()
    {
        LevelChange.OnMoveObject += MoveObject;
    }

    private void OnDisable()
    {
        LevelChange.OnMoveObject -= MoveObject;
    }

    private void MoveObject(bool moveUp, Transform gameObj)
    {
        Vector3 targetPositionSelf = transform.position + new Vector3(0, moveUp ? 1000 : -1000, 0);
        Vector3 targetPositionObj = gameObj.position + new Vector3(0, moveUp ? 1000 : -1000, 0);
        Vector3 targetPositionBg = bg.transform.position + new Vector3(0, moveUp ? 100 : -100, 0);
        Vector3 targetPositionEndBg = endBg.transform.position + new Vector3(0, moveUp ? 1000 : -1000, 0);
        Vector3 targetPositionFog = fog.transform.position + new Vector3(0, moveUp ? 1000 : -1000, 0);

        StartCoroutine(AnimateMovement(transform, targetPositionSelf, gameObj, targetPositionObj, bg.transform, targetPositionBg, endBg.transform, targetPositionEndBg, fog.transform, targetPositionFog, moveDuration));
    }

    private IEnumerator AnimateMovement(Transform startTransform, Vector3 endPositionSelf, Transform gameObjTransform, Vector3 endPositionObj, Transform bgTransform, Vector3 endPositionBg, Transform endBgTransform, Vector3 endPositionEndBg, Transform fogTransform, Vector3 endPositionFog, float duration)
    {
        float elapsedTime = 0;
        Vector3 startPositionSelf = startTransform.position;
        Vector3 startPositionObj = gameObjTransform.position;
        Vector3 startPositionBg = bgTransform.position;
        Vector3 startPositionEndBg = endBgTransform.position;
        Vector3 startPositionFog = fogTransform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            startTransform.position = Vector3.Lerp(startPositionSelf, endPositionSelf, t);
            gameObjTransform.position = Vector3.Lerp(startPositionObj, endPositionObj, t);
            bgTransform.position = Vector3.Lerp(startPositionBg, endPositionBg, t);
            endBgTransform.position = Vector3.Lerp(startPositionEndBg, endPositionEndBg, t);
            fogTransform.position = Vector3.Lerp(startPositionFog, endPositionFog, t);

            // Desactivar 'bg' si 'fog' alcanza o cruza y = 0

            // Desactivar 'fog' si 'endBg' alcanza o cruza y = 100
            if (endBgTransform.position.y == 0 && fog.activeSelf)
            {
                fog.SetActive(false);
                bg.SetActive(false);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        startTransform.position = endPositionSelf;
        gameObjTransform.position = endPositionObj;
        bgTransform.position = endPositionBg;
        endBgTransform.position = endPositionEndBg;
        fogTransform.position = endPositionFog;
    }
}
