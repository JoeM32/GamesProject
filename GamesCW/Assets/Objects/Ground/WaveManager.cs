using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Enemy[] EnemyTypes;
    public event Action<int> newWave;
    private int currentWave;
    private int livingEnemies;
    private Transform player;

    public void NewWave()//joy divison blah blah
    {
        currentWave += 1;
        newWave?.Invoke(currentWave);
        for(int i = 0; i < currentWave; i++)
        {
            Enemy enemy = Instantiate(EnemyTypes[UnityEngine.Random.Range(0,EnemyTypes.Length - 1)]);
            enemy.setPlayer(player);
            enemy.death += unitKilled;
            enemy.transform.position = new Vector3(UnityEngine.Random.Range(-10f,10f),0, UnityEngine.Random.Range(-10f,10f));
            livingEnemies += 1;
        }
    }

    private void unitKilled()
    {
        livingEnemies -= 1;
        if(livingEnemies == 0)
        {
            NewWave();
        }
    }

    //dont like this being here mut be a better way
    public void setPlayer(Transform player)
    {
        this.player = player;
    }
}
