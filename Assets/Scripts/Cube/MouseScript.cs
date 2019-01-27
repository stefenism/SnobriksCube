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
    Vector3 vecToRotateTo;
    //Rotation Vars
    // Angular speed in radians per sec.
    Vector3 startAngle;
    float step;
    float speed = 0.1f;
    bool rotateTo = false;
    float mouseVel;
    float mouseVelTime;
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

            if (!rotateTo)
            {

                if (Input.GetMouseButtonUp(0))
                {
                    //print(objectToRotate.transform.rotation.eulerAngles.x);

                    rotateTo = true;
                    step = 0;


                    if (rotateVert)
                    {
                        mouseVel = Mathf.Clamp(Input.mousePosition.y - lastMouseY, -50f, 50f);
                    }
                    else
                    {
                        mouseVel = Mathf.Clamp(Input.mousePosition.x - lastMouseX, -40f, 40f);

                    }
                    mouseVelTime = 0.12f;
                }

                if (rotateVert)
                {
                    objectToRotate.transform.Rotate(Vector3.right * (Mathf.Clamp(Input.mousePosition.y - lastMouseY, -50f, 50f) / 5.5f));
                }
                else
                {
                    objectToRotate.transform.Rotate(Vector3.up * -(Mathf.Clamp(Input.mousePosition.x - lastMouseX, -40f, 40f) / 5.5f));

                }
            }
            else
            {
                if (mouseVelTime > 0)
                {
                    if (rotateVert)
                    {
                        objectToRotate.transform.Rotate(Vector3.right * mouseVel / 5.5f);
                    }
                    else
                    {
                        objectToRotate.transform.Rotate(Vector3.up * -mouseVel / 5.5f);

                    }
                    mouseVelTime -= Time.deltaTime;
                    startAngle = objectToRotate.transform.eulerAngles;
                    vecToRotateTo = objectToRotate.transform.eulerAngles;
                    vecToRotateTo.x = Mathf.Round(vecToRotateTo.x / 90) * 90;
                    vecToRotateTo.y = Mathf.Round(vecToRotateTo.y / 90) * 90;
                    vecToRotateTo.z = Mathf.Round(vecToRotateTo.z / 90) * 90;
                }
                else
                {


                    step += 0.1f;
                    objectToRotate.transform.eulerAngles = new Vector3(
                 Mathf.LerpAngle(startAngle.x, vecToRotateTo.x, step),
                 Mathf.LerpAngle(startAngle.y, vecToRotateTo.y, step),
                 Mathf.LerpAngle(startAngle.z, vecToRotateTo.z, step));
                    if (step >= 1)
                    {
                        //ends rotation and detaches children
                        rotating = false;
                        rotateTo = false;
                        objectToRotate.transform.DetachChildren();
                    }

                }
            }
            /*
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
           */

        }

        //gets mouse location to calculate change
        lastMouseX = Input.mousePosition.x;
        lastMouseY = Input.mousePosition.y;
    }


}
