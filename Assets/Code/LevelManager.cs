using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelManager() { }
    public static LevelManager instance { get; private set; }

    [SerializeField] private LevelConfig[] levels;

    [field: SerializeField] public LevelConfig currentLevel;

    private int levelIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            currentLevel = levels[0];
            levelIndex = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }

    public bool nextLevel()
    {
        levelIndex++;
        if (levelIndex < levels.Length)
        {
            currentLevel = levels[levelIndex];
            return true;
        }
        else
            return false;
    }
}
