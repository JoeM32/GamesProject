using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform player;
    public Vector3 offset = new Vector3(0,9.72f,-8.2f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + offset;
    }

    public void setPlayer(Player player)
    {
        this.player = player.transform;
    }
}
