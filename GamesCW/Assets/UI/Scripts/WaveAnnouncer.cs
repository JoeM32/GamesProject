using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAnnouncer : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = transform.GetComponent<Text>();
        text.text = "";
    }
    public void newWave(int wave)
    {
        StartCoroutine(waveGraphic(wave));
    }

    IEnumerator waveGraphic(int wave)
    {
        
        text.text = "Wave " + wave;
        int size = 10;
        while(size < 120)
        {
            text.fontSize = size;
            size += 2;
            yield return new WaitForEndOfFrame();
        }
        text.text = "";
    }
}
