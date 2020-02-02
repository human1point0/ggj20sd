using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICanvasManager : MonoBehaviour
{
    protected Renderer CanvasObject; // Assign in inspector
    protected RepairActions _actions;
    protected GameAdmin _admin;
    public Button[] Buttons;
    private GameObject c;

    public void setContaining(GameObject _container)
    {
        c = _container;
    }

    private void Awake()
    {
        //CanvasObject = GetComponent<Renderer>();
    }

    private void Start()
    {
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


    public virtual void ShowMenu(RepairActions raActions)
    {
        //CanvasObject.enabled = true;
        c.SetActive(true);
        Buttons[0].Select();
    }

    public void HideMenu() {
        //CanvasObject.enabled = false;
        c.SetActive(false);
    }
    public void ping()
    {
        print("ping");
    }
}