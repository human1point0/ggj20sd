using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PauseMenuManager : UICanvasManager
{

    public void ResumeCallback()
    {
        base._admin?.OnResumeGameplay();
    }

    public override void ShowMenu(RepairActions raActions)
    {
        base.ShowMenu(raActions);
        raActions.UI.PauseButton.started += ctx =>
        {
            ResumeCallback();
        };
    }

    public void ExitCallback()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}