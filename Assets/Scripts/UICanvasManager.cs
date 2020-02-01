using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasManager : MonoBehaviour
{
    private Canvas CanvasObject; // Assign in inspector
    private RepairActions _actions;
    private void Awake()
    {
        CanvasObject = GetComponent<Canvas>();
    }

    public void setRepairActionsRef(RepairActions actions)
    {
        _actions = actions;
        _actions.UI.Navigate.started += ctx => {
            OnNavigation(_actions.UI.Navigate.ReadValue<Vector2>());
        };
    }

    private void OnNavigation(Vector2 vec)
    {
        if(vec.x > 0) //going right
        {
            LeftActionable();
            return;
        } else if (vec.x < 0) //going left
        {
            RightActionable();
            return;
        }

        if (vec.y > 0) //going up
        {
            UpActionable();
        } else //going down
        {
            DownActionable();
        }
    }

    private void LeftActionable()
    {
        print("UP");
    }
    private void RightActionable()
    {
        print("DOWN");
    }

    private void UpActionable() {
        print("UP");
    }
    private void DownActionable()
    {
        print("DOWN");
    }


    public void ShowMenu(GameAdmin.GameState state = GameAdmin.GameState.StartMenu)
    {
        CanvasObject.enabled = true;
    }

    public void HideMenu()
    {
        CanvasObject.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        HideMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
