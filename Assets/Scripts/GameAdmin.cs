using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//admins the game state
public class GameAdmin : MonoBehaviour
{

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
