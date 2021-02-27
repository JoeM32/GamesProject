using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    InputField console;
    Player player;

    private void Awake()
    {
        console = transform.GetComponent<InputField>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Command(console.text);
            console.text = "";
        }
    }

    public void setPlayer(Player player)
    {
        this.player = player;
    }

    void Command(string command)
    {
        switch(command)
        {
            case "help":
                Debug.Log(
                    "1 -\"help\": You already know this one!\n" +
                    "2 -\"immortality\": Negative player health never ends the game.\n" +
                    "3 -\"pause\": Sets Time.deltaTime to 0, hopefully pausing the game.\n" +
                    "4 -\"unpause\": Sets Time.deltaTime to 1, hopefully unpausing the game.\n" +
                    "5 -\"end\": Ends the game.\n" +
                    "6 -\"hurt\": Take some damage."
                    );
                break;
            case "immortality":
                player.immortality = !player.immortality;
                Debug.Log("IMMORTALITY -" + player.immortality);
                break;
            case "pause":
                Debug.Log("PAUSED");
                Time.timeScale = 0;
                break;
            case "unpause":
                Debug.Log("UNPAUSED");
                Time.timeScale = 1;
                break;
            case "hurt":
                Debug.Log("FEELING THE PAIN");
                player.CurrentHealth -= 10;
                break;
            case "end":
                Debug.Log("DEATH");
                player.CurrentHealth = -1;
                break;
            default:
                Debug.Log("Unkown Command\n" +
                    "Type \"help\" for more commands.");
                break;
        }
    }
}
