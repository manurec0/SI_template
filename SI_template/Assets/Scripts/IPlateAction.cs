using UnityEngine;

public interface IPlateAction
{
    void ExecuteAction(bool isActive);
    void SetOnPause(bool pause);
}
