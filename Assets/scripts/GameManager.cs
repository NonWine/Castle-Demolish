using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _confettiFx;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _weaponPanel;
    [SerializeField] private GameObject _TutorialPanel;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private TotalHealht _healthManager;
    [SerializeField] private Unlocking unlockManager;
    private bool isFinish;
    private int isLoop;
    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        isLoop = PlayerPrefs.GetInt("isLoop",0);
    }

    private void Start()
    {
        int x = PlayerPrefs.GetInt("currentUnlock");
        Debug.Log(x);
        for (int i = 0; i < x; i++)
        {
            _buttons[i].SetActive(true);
        }
        _gamePanel.SetActive(true);
        _levelText.SetText("LEVEL " + (LevelManager.Instance.VisualCurrentLevel).ToString());
        if (isLoop == 0)
        {
            Invoke(nameof(StartTutorial), 1.5f);
          
        }
    }

    public void GameLose()
    {
        if (isFinish)
            return;
        isFinish = true;
        _losePanel.SetActive(true);
        _gamePanel.SetActive(false);
        foreach (var item in _buttons)
        {
            item.GetComponentInChildren<Button>().interactable = true;
        }
    }

    public void GameWin()
    {
       
        isLoop = 1;
        PlayerPrefs.SetInt("isLoop", isLoop);
        Debug.Log("GAMEWIN");
        if (isFinish)
            return;
        isFinish = true;
        LevelManager.Instance.FinishLevel();
        if(_confettiFx != null)
        _confettiFx.SetActive(true);
        _gamePanel.SetActive(false);
        _winPanel.SetActive(true);
        Debug.Log(unlockManager.IsAllUnlock());
        if (unlockManager.IsAllUnlock() == 0)
            unlockManager.Fill(50);
        else
            _weaponPanel.SetActive(false);
        foreach (var item in _buttons)
        {
            item.GetComponentInChildren<Button>().interactable = true;
        }
    }

    public void NextLevel()
    {
        Debug.Log(unlockManager.Value());
        isFinish = false;
        _healthManager.ResetHp();
        if (unlockManager.Value() == 1f && unlockManager.IsAllUnlock() == 0)
            unlockManager.Unlock();
        _winPanel.SetActive(false);
        _gamePanel.SetActive(true);
        for (int i = 0; i < BulletsList.GetAmount(); i++)
        {
            if (BulletsList.Getweapon(i) != null)
                BulletsList.Getweapon(i).SetActive(false);
        }
        LevelManager.Instance.LoadLevel();
        _levelText.SetText("LEVEL " + (LevelManager.Instance.VisualCurrentLevel).ToString());

    }

    public void RestartLevel()
    {
        isFinish = false;
        _gamePanel.SetActive(true);
        _losePanel.SetActive(false);
     
        LevelManager.Instance.LoadLevel();
    }
    public bool GetFinish()
    {
        return isFinish;
    }
    private void StartTutorial()
    {
        _TutorialPanel.SetActive(true);
    }
}