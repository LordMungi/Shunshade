using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask grabPlaneLayer;
    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private LayerMask interactionLayer;

    [SerializeField] private Parasol parasol;
    [SerializeField] private LevelConfig level;

    [SerializeField] private float grabbedObjectHeight = 1f;

    [Header("Broadcast Events")]
    [SerializeField] EventChannel GeneratorClickedEvent;
    [SerializeField] private EventChannel PauseGameEvent;
    [SerializeField] private EventChannel UnpauseGameEvent;

    private const float RAY_MAX_LENGTH = 10000f;

    private Item grabbedObject;
    private Rigidbody grabbedObjectBody;

    private bool isPaused;
    private int correctItems;

    void Start()
    {
        Time.timeScale = 1;
        foreach (Item i in level.items)
        {
            foreach (Tag t in i.config.Tags)
            {
                if (t == level.correctTag)
                {
                    correctItems++;
                    break;
                }
            }
        }
        Debug.Log("Correct items: " + correctItems);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, RAY_MAX_LENGTH, grabbableLayer | interactionLayer))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("Generator"))
                    {
                        GeneratorClickedEvent.RaiseEvent();
                    }
                    else
                    {
                        grabbedObject = hit.transform.GetComponent<Item>();
                        if (grabbedObject.hasSomethingOnTop)
                            grabbedObject = null;

                        else
                        {
                            grabbedObjectBody = hit.transform.GetComponent<Rigidbody>();
                            if (grabbedObjectBody != null)
                            {
                                grabbedObjectBody.isKinematic = true;
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseGrabbedObject();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();               
            }
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

    public void CheckVictory()
    {
        int checkedCorrect = 0;
        int itemsChecked = 0;

        foreach (Item i in parasol.itemsOnShade)
        {
            foreach (Tag t in i.config.Tags)
            {
                if (t == level.correctTag)
                {
                    checkedCorrect++;
                    break;
                }
            }
            itemsChecked++;
        }

        if (checkedCorrect >= correctItems && itemsChecked <= checkedCorrect)
            Debug.Log("Win");
        else
            Debug.Log("Missing");
    }

    public void PauseGame()
    {
        isPaused = true;
        PauseGameEvent.RaiseEvent();
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        isPaused = false;
        UnpauseGameEvent.RaiseEvent();
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
