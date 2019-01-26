using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = new Vector3(5,15,15);
        transform.RotateAround(center, Vector3.up, 20 * Time.deltaTime);
    }
}
