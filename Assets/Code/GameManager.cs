using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask grabPlaneLayer;
    [SerializeField] private LayerMask grabbableLayer;

    [SerializeField] private float grabbedObjectHeight = 1f;

    private const float RAY_MAX_LENGTH = 10000f;

    private GameObject grabbedObject;
    private Rigidbody grabbedObjectBody;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, RAY_MAX_LENGTH, grabbableLayer))
            {
                if (hit.collider != null)
                {
                    grabbedObjectBody = hit.transform.GetComponent<Rigidbody>();
                    if (grabbedObjectBody != null)
                    {
                        grabbedObject = hit.transform.gameObject;
                        grabbedObjectBody.isKinematic = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseGrabbedObject();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
            MoveGrabbedObject();
    }

    private void MoveGrabbedObject()
    {
        if (grabbedObject != null)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, RAY_MAX_LENGTH, grabPlaneLayer))
                grabbedObjectBody.MovePosition(hit.point + new Vector3(0, grabbedObjectHeight + grabbedObject.transform.lossyScale.y / 2));
            else
                ReleaseGrabbedObject();
        }

    }

    private void ReleaseGrabbedObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject = null;
            grabbedObjectBody.isKinematic = false;
            grabbedObjectBody.linearVelocity = Vector3.zero;
            grabbedObjectBody = null;
        }
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
