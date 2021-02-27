using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public UIManager uiManager;
    public InputManager inputManager;
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
    }
}
