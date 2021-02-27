using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StaminaBar staminaBar;
    public HealthBar healthBar;
    public DebugLogger debugger;
    public Console console;
    public MainMenu mainMenu;
    public EndScreen endScreen;
    // Start is called before the first frame update
    void Awake()
    {
        mainMenu = Instantiate(mainMenu, transform);
        mainMenu.start += BuildGameUI;
    }

    void BuildGameUI()
    {
        mainMenu.gameObject.SetActive(false);
        healthBar = Instantiate(healthBar, transform);
        staminaBar = Instantiate(staminaBar, transform);
        debugger = Instantiate(debugger, transform);
        console = Instantiate(console, transform);
    }

    public void setPlayer(Player player)
    {
        player.healthChange += healthBar.OnHealthChange;
        player.staminaChange += staminaBar.OnStaminaChange;
        console.setPlayer(player);
        player.death += OnDeath;
    }

    public void OnDeath()
    {
        endScreen = Instantiate(endScreen, transform);
        healthBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(false);
        debugger.gameObject.SetActive(false);
        console.gameObject.SetActive(false);
    }
}
