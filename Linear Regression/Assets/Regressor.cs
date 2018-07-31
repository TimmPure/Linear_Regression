using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regressor : MonoBehaviour {

    public List<Vector2> dataSet;
    public float m = 0f;
    public float b = 0f;
    public float xBar = 0f;
    public float yBar = 0f;
    public float nom = 0f;
    public float den = 0f;
    private LineRenderer lr;
    private TextUpdate text; 

	void Start () {
        dataSet = new List<Vector2>();
        lr = GetComponent<LineRenderer>();
        if(lr == null)
        {
            Debug.LogError("Could not find LineRenderer on " + this);
        }
        text = FindObjectOfType<TextUpdate>();
        if (text == null)
        {
            Debug.LogError("Could not find TextUpdate on " + this);
        }
    }

    public void Regress()
    {
        //Can't correlate with only a single datapoint
        if (dataSet.Count <= 1)
        {
            return;
        }

        //Reset function values
        m = 0f;
        b = 0f;
        nom = 0f;
        den = 0f;
        float xBar = XBar();
        float yBar = YBar();

        //Calculate nominator and denominator for m
        for (int i = 0; i < dataSet.Count; i++)
        {
            nom += (dataSet[i].x - xBar) * (dataSet[i].y - yBar);
            den += Mathf.Pow((dataSet[i].x - xBar), 2);
        }

        m = nom / den;
        b = yBar - m * xBar;

        DrawRegression();
        text.UpdateText(m, b);
    }

    public float XBar()
    {
        float sum = 0f;
        for (int i = 0; i < dataSet.Count; i++)
        {
            sum += dataSet[i].x;
        }
        return sum / dataSet.Count;
    }

    public float YBar()
    {
        float sum = 0f;
        for (int i = 0; i < dataSet.Count; i++)
        {
            sum += dataSet[i].y;
        }
        return sum / dataSet.Count;
    }

    public void DrawRegression()
    {
        lr.enabled = true;
        Vector3[] positions = new Vector3[2] { new Vector2(-10, -10 * m + b), new Vector2(10, 10 * m + b) };
        GetComponent<LineRenderer>().SetPositions(positions);
    }
}
