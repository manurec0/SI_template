using System;

public class LevelChange
{
    public static event Action<int> OnLevelUp;
    public static event Action<bool> OnMoveObject;


    public static void TriggerMoveObject(bool moveUp)
    {
        OnMoveObject?.Invoke(moveUp);
    }

    public static void LevelUp(int newCounter)
    {
        OnLevelUp?.Invoke(newCounter);  // Safely invoke the event if there are subscribers
    }
}