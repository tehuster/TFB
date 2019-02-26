using UnityEngine;

[CreateAssetMenu]
public class StringVariable : ScriptableObject
{
    #if UNITY_EDITOR
    [Multiline]
    public string Description = "";
    #endif
    [SerializeField]
    private string value = "";

    public string Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
