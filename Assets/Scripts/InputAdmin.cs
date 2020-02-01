using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputAdmin : MonoBehaviour
{
    RepairActions controls = null;
    GameAdmin _admin = null;
    PlayerInput _playerInput = null;

    private void Awake()
    {
        print("Awakened");
        controls = new RepairActions();
    }

    private void EnableUI()
    {
        controls.UI.Enable();
    }

    private void DisableUI()
    {
        controls.UI.Disable();
    }

    private void EnablePlayer()
    {
        controls.Player.Enable();
    }

    private void DisablePlayer()
    {
        controls.Player.Disable();
    }

    public void SetUIMode()
    {
        DisablePlayer();
        EnableUI();
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void SetGameplayMode()
    {
        DisableUI();
        EnablePlayer();
        _playerInput.SwitchCurrentActionMap("Player");
    }

    public void setGameAdminReference(GameAdmin admin)
    {
        _admin = admin;
    }

    public void setPlayerInputReference(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    public void ping()
    {
        print("ping" + this.GetType().Name);
    }
    //Vector2 vec = controls.Player.MoveLeftCharacter.ReadValue<Vector2>();
}
