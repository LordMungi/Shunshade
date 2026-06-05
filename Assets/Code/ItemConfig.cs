using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Scriptable Objects/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public Tag[] Tags { get; private set; } 
}
