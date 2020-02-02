using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//admins the game state
public class GameAdmin : MonoBehaviour
{
    public GameObject inputAdminContainer;
    private InputAdmin _inputAdmin;
    public Transform ScrollWorldObject;
    public float ScrollSpeed = 1;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        _inputAdmin.SetGameplayMode();
        _state = GameState.InGame;
        _rawScore = 0;
        _pairingScore = 0;
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
                    _left.LaunchPairing();
                    _right.LaunchPairing();
                    Debug.Log("==== PAIRING ===");
                    _pairingScore = 0;
                }
            }
            else
            {
                _pairingScore = 0;
            }
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
