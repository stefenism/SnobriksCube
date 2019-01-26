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
    bool rotating = false;
    bool rotateVert;
    float rotationNeeded;
    GameObject objectToRotate;
    SelectorScript selectorScript;
    //mouseVars
    float lastMouseX = 0;
    float lastMouseY = 0;
    float totalMouseX = 0;
    float totalMouseY = 0;
    void Update()
    {
        if (!rotating)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag == "Selector")
                    {
                        selectorScript = hit.collider.gameObject.GetComponent<SelectorScript>();
                        holdingSelector = true;
                        totalMouseX = 0;
                        totalMouseY = 0;
                        print(hit.collider.name);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                holdingSelector = false;

            }
            if (holdingSelector)
            {
                totalMouseX += Input.mousePosition.x - lastMouseX;
                totalMouseY += Input.mousePosition.y - lastMouseY;
                if (Mathf.Abs(totalMouseX) > 20)
                {
                    print("Mouse Big");
                }
                if (Mathf.Abs(totalMouseY) > 20)
                {
                    Collider[] hitColliders = Physics.OverlapBox(selectorScript.cubeSideVert.transform.position, selectorScript.cubeSideVert.transform.localScale / 2, Quaternion.identity);
                    int i = 0;
                    while (i < hitColliders.Length)
                    {
                        if (hitColliders[i].tag == "RuCube")
                        {
                            hitColliders[i].gameObject.transform.SetParent(selectorScript.cubeSideVert.transform);
                            objectToRotate = selectorScript.cubeSideVert;
                            rotating = true;
                            rotateVert = true;
                            rotationNeeded = 90;
                        }
                        //Increase the number of Colliders in the array
                        i++;
                    }
                }
            }
        }
        else
        {
            if (rotateVert)
            {
                if (rotationNeeded>0){
                objectToRotate.transform.Rotate(Vector3.right * 0.1f);
                }


                
            }

        }

        //gets mouse location to calculate change
        lastMouseX = Input.mousePosition.x;
        lastMouseY = Input.mousePosition.y;
    }


}
