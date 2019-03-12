using UnityEngine;

[CreateAssetMenu]
public class MotorSpeed : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string Description = "";
#endif

    public int[] MotorsSpeed;
    public bool MotorState = false;
}