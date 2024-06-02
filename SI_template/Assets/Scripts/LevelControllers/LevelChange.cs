using System;
using UnityEngine;

public class LevelChange
{
    public static event Action<int> OnLevelUp;
    public static event Action<bool, Transform> OnMoveObject;


    public static void TriggerMoveObject(bool moveUp, Transform objTransform)
    {
        OnMoveObject?.Invoke(moveUp, objTransform);
    }
    
    public static void LevelUp(int newCounter)
    {
        OnLevelUp?.Invoke(newCounter);  
    }
}