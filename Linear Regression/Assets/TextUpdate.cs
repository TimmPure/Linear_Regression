using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour {

public void UpdateText(float m, float b)
    {
        GetComponent<Text>().text = "Y = " + Mathf.Round(m * 100f)/100f + "X + " + Mathf.Round(b * 100f) / 100f;
    }
}
