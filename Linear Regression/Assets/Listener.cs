using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{

    public Transform pointParent;
    public GameObject pointPrefab;
    private List<Vector2> dataSet;
    private Regressor regressor;

    private void Start()
    {
        regressor = FindObjectOfType<Regressor>();
        if (regressor == null)
        {
            Debug.LogError("Could not find Regressor!");
        }
        dataSet = regressor.dataSet;
        if (dataSet == null)
        {
            Debug.LogError("Could not find dataSet on Regressor!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f));
            if (Mathf.Abs(clickPos.y) <= 10f && Mathf.Abs(clickPos.x) <= 10f)
            {
                Instantiate(pointPrefab, clickPos, Quaternion.identity, pointParent);
                dataSet.Add(clickPos);
                regressor.Regress();
            } else
            {
                Debug.Log("Clicked outside the bounding box");
            }
        }
    }
}
