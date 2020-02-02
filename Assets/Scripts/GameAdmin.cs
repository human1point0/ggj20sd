using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//admins the game state
public class GameAdmin : MonoBehaviour
{
    public GameObject inputAdminContainer;
    public GameObject pauseMenuContainer;
    public GameObject startWinLoseMenuContainer;
    public GameObject scoreBarContainer;
    private InputAdmin _inputAdmin;
    private ScoreCounterScript scs;
    public Transform ScrollWorldObject;
    public float ScrollSpeed = 1;

    private UICanvasManager _pauseUI;
    private UICanvasManager _swlUI;

    [SerializeField] private PlayerController _left;
    [SerializeField] private PlayerController _right; 

    [Range(1, 10)]    
    [SerializeField] private float _scoreDistanceMultiplier = 5;

    [SerializeField] private float _scoreRateMultiplier = 1;
    private float _rawScore = 0;

    public Text scoreText;
    public enum GameState
    {
        StartMenu,
        PauseMenu,
        InGame,
        Win,
        Lose
    }

    private GameState _state = GameState.StartMenu;

    GameState getState()
    {
        return _state;
    }

    private void Awake()
    {
        _inputAdmin = inputAdminContainer.GetComponent(typeof(InputAdmin)) as InputAdmin;
        _inputAdmin.setGameAdminReference(this);
        _inputAdmin.setPlayerInputReference(inputAdminContainer.GetComponent(typeof(PlayerInput)) as PlayerInput);
        setupPauseUI();
        scs = scoreBarContainer.GetComponent(typeof(ScoreCounterScript)) as ScoreCounterScript;
    }

    private void setupPauseUI()
    {
        _pauseUI = pauseMenuContainer.GetComponent(typeof(UICanvasManager)) as UICanvasManager;
        _pauseUI.setGameAdmin(this);
        _pauseUI.setContaining(pauseMenuContainer);
        _pauseUI.setRepairActionsRef(_inputAdmin.getRepairActions());
    }


    // Start is called before the first frame update
    void Start()
    {
        _inputAdmin.SetGameplayMode();
        _state = GameState.InGame;
        /*_inputAdmin.SetUIMode();
        _pauseUI.ShowMenu();*/
    }

    public void OnSpawnPauseMenu()
    {
        if (_state == GameState.InGame)
        {
            print("pause");
            _state = GameState.PauseMenu;
            _inputAdmin.SetUIMode();
            Time.timeScale = 0.0f;
            _pauseUI.ShowMenu();
            return;
        }
       /* } else
        {
            OnResumeGameplay();
        }     */   
    }

    public void OnResumeGameplay()
    {
        _state = GameState.InGame;
        print("unpause");
        Time.timeScale = 1.0f;
        _inputAdmin.SetGameplayMode();
        _pauseUI.HideMenu();
    }


    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case GameState.InGame:
                DoGameUpdate();
                break;
        }        
    }

    bool CheckWin()
    {
        if (_left.Win && _right.Win)
        {
            _state = GameState.Win;
            Debug.Log("Win");
        }
        return _state == GameState.Win;
    }

    bool CheckLose()
    {
        if (_left.Lost || _right.Lost)
        {
            _state = GameState.Lose;
            Debug.Log("Lost");
        }
        return _state == GameState.Lose;
    }
    void DoGameUpdate()
    {
        if (CheckWin() || CheckLose())
        {
            return;
        }
        if (ScrollWorldObject)
        {
            var speed = Time.deltaTime * ScrollSpeed;
            ScrollWorldObject.position += Vector3.back * speed;
        }
        else
        {
            Debug.Log("Connect scroll object");
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        if (_left && _right)
        {
            var diff = _scoreDistanceMultiplier - 
                       Mathf.Abs(_left.transform.position.z - _right.transform.position.z);
            if (diff > 0)
            {
                _rawScore += diff * Time.deltaTime * _scoreRateMultiplier;
            }

            scs?.SetScore(_rawScore);
            scoreText.text = $"Score: {_rawScore:0000}";
        }
        else
        {
            Debug.Log("Players not connected to GameAdmin");
        }
    }

    public void ping()
    {
        print("ping" + this.GetType().Name);
    }
}
