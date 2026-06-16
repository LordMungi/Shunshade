using UnityEngine;

public class Item : MonoBehaviour
{
    static public string GrabbableTag = "Grabbable";

    private void Start()
    {
        tag = GrabbableTag;
        foreach (Transform child in transform)
        {
            child.tag = GrabbableTag;
        }
    }

}
