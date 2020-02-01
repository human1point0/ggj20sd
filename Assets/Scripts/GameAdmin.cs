using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//admins the game state
public class GameAdmin : MonoBehaviour
{
    public Transform ScrollWorldObject;
    public float ScrollSpeed = 1;
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
    // Start is called before the first frame update
    void Start()
    {
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
    }
}
