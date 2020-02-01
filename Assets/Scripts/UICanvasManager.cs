using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICanvasManager : MonoBehaviour
{
    private Canvas CanvasObject; // Assign in inspector
    private RepairActions _actions;
    private GameAdmin _admin;
    public Button[] Buttons;
    private void Awake()
    {
        CanvasObject = GetComponent<Canvas>();
        HideMenu();
    }

    public void setRepairActionsRef(RepairActions actions)
    {
        _actions = actions;
    }
    public void setGameAdmin(GameAdmin admin)
    {
        _admin = admin;
    }


    public void ShowMenu(GameAdmin.GameState state = GameAdmin.GameState.StartMenu)
    {
        CanvasObject.enabled = true;
        Buttons[0].Select();
    }

    public void HideMenu() { 
        CanvasObject.enabled = false;
    }
    public void ping()
    {
        print("ping");
    }
}