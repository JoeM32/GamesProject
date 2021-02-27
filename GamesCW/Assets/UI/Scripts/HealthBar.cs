using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public void OnHealthChange(float percentage)
    {
        transform.localScale = new Vector3(percentage, 1, 1);
    }
}
