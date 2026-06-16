using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float RAY_MAX_LENGTH = 100f;

    private GameObject grabbedObject;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, RAY_MAX_LENGTH))
            {
                if (hit.collider != null)
                {
                    grabbedObject = hit.transform.gameObject;
                    
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            grabbedObject = null;
        }

        if (Input.GetMouseButton(0))
            MoveGrabbedObject();
    }

    private void MoveGrabbedObject()
    {
        grabbedObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Input.GetMouseButton(0))
        {
            Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
    }
}
