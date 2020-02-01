﻿using System.Collections;
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

    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        _inputAdmin.SetGameplayMode();
        _state = GameState.InGame;
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

    void DoGameUpdate()
    {
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
            var diff = _scoreDistanceMultiplier - Mathf.Abs(_left.position.z - _right.position.z);
            if (diff > 0)
            {
                _rawScore += diff * Time.deltaTime * _scoreRateMultiplier;
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
