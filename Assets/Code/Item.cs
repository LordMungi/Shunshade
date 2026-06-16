using UnityEngine;

public class Item : MonoBehaviour
{
    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Grabbable");
    }

}
