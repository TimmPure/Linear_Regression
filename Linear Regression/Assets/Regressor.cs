using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regressor : MonoBehaviour {

    public List<Vector2> dataSet;
    public float m = 0f;
    public float b = 0f;
    public float m_hat = 0f;
    public float b_hat = 0f;
    public float x_bar = 0f;
    public float y_bar = 0f;
    public float nom = 0f;
    public float den = 0f;
    public float learningRate = .01f;
    private LineRenderer OLSLine;
    private LineRenderer gradientLine;
    private TextUpdate text; 

	void Start () {
        dataSet = new List<Vector2>();
        OLSLine = GetComponent<LineRenderer>();
        if(OLSLine == null)
        {
            Debug.LogError("Could not find LineRenderer on " + this);
        }
        gradientLine = GameObject.FindGameObjectWithTag("GradientLine").GetComponent<LineRenderer>();
        if (OLSLine == null)
        {
            Debug.LogError("Could not find LineRenderer on " + this);
        }
        text = FindObjectOfType<TextUpdate>();
        if (text == null)
        {
            Debug.LogError("Could not find TextUpdate on " + this);
        }
    }

    public void LeastSquares()
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
        x_bar = XBar();
        y_bar = YBar();

        //Calculate nominator and denominator for m
        for (int i = 0; i < dataSet.Count; i++)
        {
            nom += (dataSet[i].x - x_bar) * (dataSet[i].y - y_bar);
            den += Mathf.Pow((dataSet[i].x - x_bar), 2);
        }

        m = nom / den;
        b = y_bar - m * x_bar;

        DrawOLS();
        text.UpdateText(m, b);
    }

    public void GradientDescent()
    {
        //Can't correlate with only a single datapoint
        if (dataSet.Count <= 1)
        {
            return;
        }

        for (int i = 0; i < dataSet.Count; i++)
        {
            float x = dataSet[i].x;
            float y = dataSet[i].y;
            float y_hat = m_hat * x + b_hat;
            float error = y - y_hat;

            m_hat += error * x * learningRate;
            b_hat += error * learningRate;
        }
        Debug.Log("m_hat is " + m_hat + ", b_hat = " + b_hat);

        DrawGradientLine();
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

    public void DrawOLS()
    {
        OLSLine.enabled = true;
        Vector3[] positions = new Vector3[2] { new Vector2(-10, -10 * m + b), new Vector2(10, 10 * m + b) };
        OLSLine.SetPositions(positions);
    }

    public void DrawGradientLine()
    {
        gradientLine.enabled = true;
        Vector3[] positions = new Vector3[2] { new Vector2(-10, -10 * m_hat + b_hat), new Vector2(10, 10 * m_hat + b_hat) };
        gradientLine.SetPositions(positions);
    }
}
