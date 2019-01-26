using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    //raycast vars
    Ray ray;
    RaycastHit hit;


    bool holdingSelector = false;
    bool rotating = false;
    bool rotateVert;
    float rotateDirection = -1;// 1 or -1
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
        if (!rotating)//dont allow clicks if rotating
        {
            //raycast for clicks
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //checks if it hit a selector
                    if (hit.collider.tag == "Selector")
                    {
                        //starts the recording of mouse inputs so we can see what direction is rotated
                        selectorScript = hit.collider.gameObject.GetComponent<SelectorScript>();
                        holdingSelector = true;
                        totalMouseX = 0;
                        totalMouseY = 0;
                    }
                }
            }
            //cancels recording of mouse if mouse is released
            if (Input.GetMouseButtonUp(0))
            {
                holdingSelector = false;

            }
            if (holdingSelector)//records mouse inputs and checks if it has moved far enough
            {
                totalMouseX += Input.mousePosition.x - lastMouseX;
                totalMouseY += Input.mousePosition.y - lastMouseY;
                if (Mathf.Abs(totalMouseX) > 20)/////////////////////HORIZONTAL ROTATION/////////////////////////////////
                {
                    //gets all objects collided with the cube row
                    Collider[] hitColliders = Physics.OverlapBox(selectorScript.cubeSideHor.transform.position, selectorScript.cubeSideHor.transform.localScale / 2, Quaternion.identity);
                    int i = 0;
                    while (i < hitColliders.Length)
                    {
                        if (hitColliders[i].tag == "RuCube")//checks if its a cube 
                        {
                            hitColliders[i].gameObject.transform.SetParent(selectorScript.cubeSideHor.transform);//parents the cube to the row
                            //sets the cubes to rotate
                            objectToRotate = selectorScript.cubeSideHor;
                            rotating = true;
                            rotateVert = false;
                            rotationNeeded = 90;
                            holdingSelector = false;
                            //gets the direction to rotate
                            if (totalMouseX > 0)
                            {
                                rotateDirection = -1f;
                            }
                            else
                            {
                                rotateDirection = 1f;
                            }
                        }
                        //Increase the number of Colliders in the array
                        i++;
                    }
                }
                if (Mathf.Abs(totalMouseY) > 20)/////////////////////VERTICLE ROTATION/////////////////////////////////
                {
                    //gets all objects collided with the cube row
                    Collider[] hitColliders = Physics.OverlapBox(selectorScript.cubeSideVert.transform.position, selectorScript.cubeSideVert.transform.localScale / 2, Quaternion.identity);
                    int i = 0;
                    while (i < hitColliders.Length)
                    {
                        if (hitColliders[i].tag == "RuCube")//checks if its a cube 
                        {
                            hitColliders[i].gameObject.transform.SetParent(selectorScript.cubeSideVert.transform);//parents the cube to the row
                            //sets the cubes to rotate
                            objectToRotate = selectorScript.cubeSideVert;
                            rotating = true;
                            rotateVert = true;
                            rotationNeeded = 90;
                            holdingSelector = false;
                            //gets the direction to rotate
                            if (totalMouseY > 0)
                            {
                                rotateDirection = 1f;
                            }
                            else
                            {
                                rotateDirection = -1f;
                            }
                        }
                        //Increase the number of Colliders in the array
                        i++;
                    }
                }
            }
        }
        else
        {
            if (rotationNeeded > 0)//rotates cubes over time
            {
                float changeSpeed = 5;//NEEDS TO BE A MULTAPUL OF 90
                //checks if rows or colums need to be rotated
                if (rotateVert)
                {
                    objectToRotate.transform.Rotate(Vector3.right * changeSpeed * rotateDirection);
                }
                else
                {
                    objectToRotate.transform.Rotate(Vector3.up * changeSpeed * rotateDirection);
                }
                rotationNeeded += -changeSpeed;
            }
            else
            {
                //ends rotation and detaches children
                rotating = false;
                objectToRotate.transform.DetachChildren();
            }


        }

        //gets mouse location to calculate change
        lastMouseX = Input.mousePosition.x;
        lastMouseY = Input.mousePosition.y;
    }


}
