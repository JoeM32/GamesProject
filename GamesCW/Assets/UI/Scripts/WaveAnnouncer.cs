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


    //Needs work below - a full ui squidger for juice preferably

    IEnumerator waveGraphic(int wave)
    {
        
        text.text = "Wave " + wave;
        int size = 10;
        while(size < 120)
        {
            text.fontSize = size;
            size += 1;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.2f);
        text.text = "";
    }
}
