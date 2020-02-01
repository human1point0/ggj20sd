using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAdmin : MonoBehaviour
{
    RepairActions controls = null;

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
    }

    private void DisablePlayer()
    {
        controls.Player.Disable();
    }

    //Vector2 vec = controls.Player.MoveLeftCharacter.ReadValue<Vector2>();
}
