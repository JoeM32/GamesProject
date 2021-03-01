using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public UIManager uiManager;
    public InputManager inputManager;
    public WaveManager waveManager;
    public Player player;
    private void Awake()
    {
        uiManager = Instantiate(uiManager);
        uiManager.mainMenu.start += startGame;
        
    }

    void startGame()
    {
        player = Instantiate(player,new Vector3(0,0,0), Quaternion.Euler(Vector3.zero));
        uiManager.setPlayer(player);
        inputManager = Instantiate(inputManager);
        inputManager.setPlayer(player);
        CameraControl cameraControl = Camera.main.gameObject.AddComponent<CameraControl>();
        cameraControl.setPlayer(player);
        waveManager = Instantiate(waveManager);
        waveManager.setPlayer(player.transform);
        waveManager.newWave += uiManager.waveAnnouncer.newWave;
        waveManager.NewWave();
        Debug.Log("The red and blue circles are for debugging and serve no purpose but to makr out of range in in range dodges. At release ideally a ring will will appear to remind you where you can dodge to, and the circles will be repalced by vfx");
    }
}
