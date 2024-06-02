using System;
using UnityEngine;

public class LevelChange
{
    public static event Action<int> OnLevelUp;
    public static event Action<bool, Transform> OnMoveObject;
    public static event Action<bool> OnActivateMovingTile;
    public static event Action OnColliderDetect;

    public static void ColliderDetect()
    {
        OnColliderDetect?.Invoke();
    }
    public static void ActivateMovingTile(bool active)
    {
        OnActivateMovingTile?.Invoke(active);
    }

    public static void TriggerMoveObject(bool moveUp, Transform objTransform)
    {
        OnMoveObject?.Invoke(moveUp, objTransform);
    }
    
    public static void LevelUp(int newCounter)
    {
        OnLevelUp?.Invoke(newCounter);  
    }
}