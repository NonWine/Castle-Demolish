using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private List<GameObject> _levels;
    [SerializeField] private int testLevelIndex;
    [SerializeField] private GameObject _tutor;
    [SerializeField] private shoot weaponManager;
    public int CurrentLevel { private set; get; }
    public int VisualCurrentLevel { private set; get; }

    private GameObject level;
    private int isLoopUnlock;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        isLoopUnlock = PlayerPrefs.GetInt("isLoopUnlock", isLoopUnlock);
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (CurrentLevel == 0)
            VisualCurrentLevel = 0;
        VisualCurrentLevel = PlayerPrefs.GetInt("VisualCurrentLevel",1);

    }

    private void Start()
    {
        if (VisualCurrentLevel == 3 && isLoopUnlock == 0)
        {
            _tutor.SetActive(true);
        }
    
        if (CurrentLevel >= _levels.Count)
            CurrentLevel = 0;
        level = Instantiate(_levels[CurrentLevel]);
    
    }

    public void FinishLevel()
    {
        CurrentLevel++;
        VisualCurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.SetInt("VisualCurrentLevel", VisualCurrentLevel);
    }

    public void LoadLevel()
    {
        
        Destroy(level);
        if (CurrentLevel >= _levels.Count)
        {
            CurrentLevel = 0;
            VisualCurrentLevel = 1;
        }
        if (VisualCurrentLevel > 3)
        {
            isLoopUnlock = 1;
            PlayerPrefs.SetInt("isLoopUnlock", isLoopUnlock);
        }
        level = Instantiate(_levels[CurrentLevel]);
        CameraMoving.Instance.ResetPositionCamera();
        if (VisualCurrentLevel == 3 && isLoopUnlock == 0)
        {
            _tutor.SetActive(true);
         
        }
        weaponManager.SetDefaultWeapon();

    }

    [ContextMenu("SetTestLevel")]
    public void SetTestLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", testLevelIndex);
    }
}