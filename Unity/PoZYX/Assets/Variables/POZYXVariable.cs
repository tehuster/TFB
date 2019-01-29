using UnityEngine;

[CreateAssetMenu]
public class POZYXVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string Description = "";
#endif

    public float x;
    public float y;
    public float z;
    public float yaw;
    public float pitch;
    public float roll;
}
