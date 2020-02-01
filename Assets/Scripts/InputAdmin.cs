﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputAdmin : MonoBehaviour
{
    RepairActions controls = null;
    GameAdmin _admin = null;
    PlayerInput _playerInput = null;

    [SerializeField]
    private PlayerController _left;
    [SerializeField]
    private PlayerController _right;
    
    private void Awake()
    {
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
        _left.SetRepairActions(controls);
        _right.SetRepairActions(controls);
    }

    private void DisablePlayer()
    {
        controls.Player.Disable();
        _left.SetRepairActions(null);
        _right.SetRepairActions(null);
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

    public RepairActions getRepairActions()
    {
        return controls;
    }

    public void setGameAdminReference(GameAdmin admin)
    {
        _admin = admin;
    }

    public void setPlayerInputReference(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    public void leftAxisHandler(Vector2 vec2)
    {
        ping();
    }

    public void rightAxisHandler(Vector2 vec2)
    {
        ping();
    }

    public void Update()
    {
        //print(controls.Player.MoveLeftCharacter.ReadValue<Vector2>().ToString());
        Vector2 vecLeft = controls.Player.MoveLeftCharacter.ReadValue<Vector2>();
        //Vector2 vecRight = controls.Player.MoveRightCharacter.ReadValue<Vector2>();
        /*if(_admin.getState() != GameAdmin.GameState.InGame)
        {
            
        }*/

    }

    public void ping()
    {
        print("ping" + this.GetType().Name);
    }
    
    
    //Vector2 vec = controls.Player.MoveLeftCharacter.ReadValue<Vector2>();
}
