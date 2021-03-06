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
        _left?.SetRepairActions(controls);
        _right?.SetRepairActions(controls);
    }

    private void DisablePlayer()
    {
        controls.Player.Disable();
        _left?.SetRepairActions(null);
        _right?.SetRepairActions(null);
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
    }

    public void ping()
    {
        print("ping" + this.GetType().Name);
    }
}
