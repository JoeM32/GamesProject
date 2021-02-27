using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public event Action<int> newWave;
    private int currentWave;
    private void NewWave()//joy divison blah blah
    {
        currentWave += 1;
        newWave?.Invoke(currentWave);
    }
}
