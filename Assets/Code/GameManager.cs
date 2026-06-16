using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask grabPlaneLayer;
    [SerializeField] private LayerMask grabbableLayer;

    private const float RAY_MAX_LENGTH = 10000f;

    private GameObject grabbedObject;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, RAY_MAX_LENGTH, grabbableLayer))
            {
                Debug.Log("hit: " + hit.transform.name);
                if (hit.collider != null)
                {
                    Debug.Log("grab");
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, RAY_MAX_LENGTH, grabPlaneLayer))
            Debug.Log(hit.point);

        if (grabbedObject != null)
            grabbedObject.transform.position = hit.point + new Vector3(0, grabbedObject.transform.lossyScale.y / 2);
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
