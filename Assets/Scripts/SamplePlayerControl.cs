using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SamplePlayerControl : MonoBehaviour
{
    RepairActions controls = null;

    private void Awake()
    {
        controls = new RepairActions();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //print(controls.Player.MoveLeftCharacter.ReadValue<Vector2>().ToString());
        Vector2 vec = controls.Player.MoveLeftCharacter.ReadValue<Vector2>();
        if (Mathf.Abs(vec.x) == 1.0f)
        {
            print("max x");
        }
        if (Mathf.Abs(vec.y) == 1.0f)
        {
            print("max y");
        }
    }

    public void Fire()
    {
        print("Fired");
    }
}
