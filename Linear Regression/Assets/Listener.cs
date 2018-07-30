using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour {

    public Transform pointParent;
    public GameObject pointPrefab;

	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 clickPos;
            clickPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f));
            Instantiate(pointPrefab, clickPos, Quaternion.identity, pointParent);
        }
	}
}
