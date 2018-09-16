using UnityEngine;
using System.Collections;

public class waypoints : MonoBehaviour {
    public static Transform[] points;


    //to keep Track of our WavePoints (Road To the tower)
    void Awake()
    {
        points = new Transform[transform.childCount]; 
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

	
}
