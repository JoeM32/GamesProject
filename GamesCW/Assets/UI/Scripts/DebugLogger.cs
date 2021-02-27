using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogger : MonoBehaviour
{
    // Start is called before the first frame update

    public int debugCount = 5;
    private int total = 0;
    public GameObject statement;
    Queue<GameObject> statements;

    private void Awake()
    {
        statement.GetComponent<Text>().text = "";
        statements = new Queue<GameObject>();
        Application.logMessageReceived += HandleLog;
        
    }
    public void OnDebug(string text)
    {
        GameObject newStatement = Instantiate(statement,transform);
        
        newStatement.GetComponent<Text>().text = total++ + "::" + text;

        foreach (GameObject g in statements)
        {
            g.transform.position += new Vector3(0, newStatement.GetComponent<Text>().preferredHeight, 0);
        }
        statements.Enqueue(newStatement);
        while (statements.Count > debugCount)
        {
            Destroy(statements.Dequeue());
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
       OnDebug(logString);
    }
}
