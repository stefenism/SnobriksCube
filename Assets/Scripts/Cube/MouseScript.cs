using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    
    Ray ray;
    RaycastHit hit;
    
    bool holdingSelector = false;

    //mouseVars
    float lastMouseX=0;
    float lastMouseY=0;
    float totalMouseX=0;
    float totalMouseY=0;
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            { 
                if (hit.collider.tag == "Selector")
                {
                    holdingSelector=true;
                    totalMouseX=0;
                    totalMouseY=0;
                    print(hit.collider.name);
                }
            }
        }
        if (Input.GetMouseButtonUp(0)){
            holdingSelector=false;
        }
        if(holdingSelector){
            totalMouseX+=Input.mousePosition.x-lastMouseX;
            totalMouseY+=Input.mousePosition.y-lastMouseY;
            if(Mathf.Abs(totalMouseX)>20){
                print("Mouse Big");
            }
        }

        lastMouseX=Input.mousePosition.x;
        lastMouseY=Input.mousePosition.y;
    }


}
