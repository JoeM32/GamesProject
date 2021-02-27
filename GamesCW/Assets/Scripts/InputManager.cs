using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Player player;
    public float playerbubble; //area aroundp layer in percent of screen that counts as clicking on the player
    public float parryTimeout = 2;//how lnog before it isint counted as a parry but as an attack.
    public float timeout = 5;//how long before the eintire interaction is discrard
    Ray debug;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine("TouchManager");
        }
    }

    IEnumerator TouchManager()
    {
        float yaxis = player.transform.position.y;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float t = (yaxis - ray.origin.y) / ray.direction.y;
        Vector3 position = ray.origin + (t * ray.direction);

        if (Vector3.Distance(player.transform.position,position) < player.dodgeRange)
        {
            Vector3 playerScreenpos = Camera.main.WorldToScreenPoint(player.transform.position);
            playerScreenpos.x /= Screen.width;
            playerScreenpos.y /= Screen.height;
            playerScreenpos.z = 0;
            Vector3 mouseScreenpos = Input.mousePosition;
            mouseScreenpos.x /= Screen.width;
            mouseScreenpos.y /= Screen.height;
            mouseScreenpos.z = 0;
            if (Vector3.Distance(playerScreenpos,mouseScreenpos) < playerbubble/100f)
            {
                float timer = 0;
                while(timer < timeout)
                {

                    if(!Input.GetMouseButton(0))//when mouse is up
                    {
                        if(timer < parryTimeout)
                        {
                            Debug.Log("Parrying");
                            player.Parry();
                        }
                        else
                        {

                        }
                        yield break;
                    }
                    timer += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log("Cancelled");
            }
            else
            {
                Debug.Log("Dodging");
                yield return ping(position, Color.blue);
            }    
        }
        else
        {
            yield return ping(position,Color.red);
            Debug.Log("Out of Range");
        }
        
    }

    private Vector2 getPlayerPos()//gets the position fo the player in percentage across the screen
    {
        Vector3 playerScreenpos = Camera.main.WorldToScreenPoint(player.transform.position);
        playerScreenpos.x /= Screen.width;
        playerScreenpos.y /= Screen.height;
        return playerScreenpos;
    }

    private Vector2 getMousePos()//gets the position fo the mouse in percentage across the screen
    {
        Vector3 mouseScreenpos = Input.mousePosition;
        mouseScreenpos.x /= Screen.width;
        mouseScreenpos.y /= Screen.height;
        return mouseScreenpos;
    }

    IEnumerator ping(Vector3 position, Color color)
    {
        GameObject ping = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ping.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        ping.transform.position = position;
        yield return new WaitForSeconds(0.2f);
        Destroy(ping);
    }

    public void setPlayer(Player player)//dunno if i like these
    {
        this.player = player;
    }
}
