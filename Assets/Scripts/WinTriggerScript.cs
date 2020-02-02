using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerScript : MonoBehaviour
{
    void Awake()
    {
        this.GetComponentInChildren<Renderer>().enabled = false;

    }
}
