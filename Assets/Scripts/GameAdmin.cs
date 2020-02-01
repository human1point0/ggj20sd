using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//admins the game state
public class GameAdmin : MonoBehaviour
{
    public GameObject inputAdminContainer;
    private InputAdmin _inputAdmin;
   
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
        _inputAdmin.SetUIMode();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ping()
    {
        print("ping" + this.GetType().Name);
    }
}
