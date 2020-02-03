using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//admins the game state
public class GameAdmin : MonoBehaviour
{
    public GameObject inputAdminContainer;
    public GameObject pauseMenuContainer;
    public GameObject LoseMenuContainer;
    public GameObject scoreBarContainer;
    public GameObject cutsceneContainer;

    public int nextScene;
    
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

    [Range(0, 1)]
    [SerializeField] private float _maxPairingDistance = 0.3f;
    
    [Range(1, 5)]
    [SerializeField] private float _pairingTime = 1;

    private float _pairingScore = 0;
    private float _rawScore = 0;
    private bool hasLost = false;
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
        setupLoseUI();
        scs = scoreBarContainer.GetComponent(typeof(ScoreCounterScript)) as ScoreCounterScript;
    }

    private void setupPauseUI()
    {
        _pauseUI = pauseMenuContainer.GetComponent(typeof(UICanvasManager)) as UICanvasManager;
        _pauseUI.setGameAdmin(this);
        _pauseUI.setContaining(pauseMenuContainer);
        _pauseUI.setRepairActionsRef(_inputAdmin.getRepairActions());
    }

    private void setupLoseUI()
    {
        _swlUI = LoseMenuContainer.GetComponent(typeof(UICanvasManager)) as UICanvasManager;
        _swlUI.setGameAdmin(this);
        _swlUI.setContaining(LoseMenuContainer);
        _swlUI.setRepairActionsRef(_inputAdmin.getRepairActions());
    }


    // Start is called before the first frame update
    void Start()
    {
        _inputAdmin.SetGameplayMode();
        _state = GameState.InGame;
        _rawScore = 0;
        _pairingScore = 0;
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
            _pauseUI.ShowMenu(_inputAdmin.getRepairActions());
            return;
        }
       /* } else
        {
            OnResumeGameplay();
        }     */   
    }

    public void OnResumeGameplay()
    {
        print("unpause");
        _state = GameState.InGame;
        Time.timeScale = 1.0f;
        _inputAdmin.SetGameplayMode();
        _pauseUI.HideMenu();
    }

    IEnumerator StartCutscene()
    {

        cutsceneContainer.transform.GetChild(0).gameObject.SetActive(true);
        cutsceneContainer.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(nextScene);
    }

    private void ShowLoseMenu()
    {
        if (!hasLost) {
            print("lasssas");
            _inputAdmin.SetUIMode();
            _swlUI.ShowMenu(_inputAdmin.getRepairActions());
            hasLost = true;
        }
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
            if (cutsceneContainer != null)
            {
                _left.gameObject.SetActive(false);
                _right.gameObject.SetActive(false);
                StartCoroutine("StartCutscene");
            }
        }
        return _state == GameState.Win;
    }

    bool CheckLose()
    {
        if (_left.Lost || _right.Lost)
        {
            _state = GameState.Lose;
            Debug.Log("Lost");
            ShowLoseMenu();
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
        var distance = Mathf.Abs(_left.transform.position.z - _right.transform.position.z);
        var diff = _scoreDistanceMultiplier - distance;

        if (diff > 0)
        {
            _rawScore += diff * Time.deltaTime * _scoreRateMultiplier;
        }

        if (distance <= _maxPairingDistance)
        {
            _pairingScore += Time.deltaTime;
            if (_pairingScore > _pairingTime)
            {
                // _left.LaunchPairing();
                // _right.LaunchPairing();
                _pairingScore = 0;
            }
            scs?.SetScore(_rawScore);
        }
        else
        {
            _pairingScore = 0;
        }

        //Debug.Log($"Score: {_rawScore:0000}");
    }

    public void OnReloadPressed()
    {
        Debug.Log("Reload");
        // todo: Start Wait Coroutine
        Reload();
    }
    void Reload()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ping()
    {
        print("ping" + this.GetType().Name);
    }
}
