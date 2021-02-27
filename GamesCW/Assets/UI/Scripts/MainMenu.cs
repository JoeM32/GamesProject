using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public event Action start;

    public void OnStartButton()
    {
        start?.Invoke();
    }
}
