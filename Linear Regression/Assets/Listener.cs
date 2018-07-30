using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{

    public Transform pointParent;
    public GameObject pointPrefab;
    private List<Vector2> dataSet;

    private void Start()
    {
        dataSet = GameObject.FindObjectOfType<Regressor>().dataSet;
        if (dataSet == null)
        {
            Debug.LogError("Could not find Regressor!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f));
            if (Mathf.Abs(clickPos.y) <= 10f && Mathf.Abs(clickPos.x) <= 10f)
            {
                Instantiate(pointPrefab, clickPos, Quaternion.identity, pointParent);
                dataSet.Add(clickPos);
            }
            else
            {
                Debug.Log("Clicked outside the bounding box");
            }
        }
    }
}
