using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{

    public Transform pointParent;
    public GameObject pointPrefab;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f));
            if (Mathf.Abs(clickPos.y) <= 10f && Mathf.Abs(clickPos.x) <= 10f)
            {
                Instantiate(pointPrefab, clickPos, Quaternion.identity, pointParent);
            }
            else
            {
                Debug.Log("Clicked outside the counding box");
            }
        }
    }
}
