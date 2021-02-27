using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public void OnStaminaChange(float percentage)
    {
        transform.localScale = new Vector3(percentage, 1, 1);
    }
}
