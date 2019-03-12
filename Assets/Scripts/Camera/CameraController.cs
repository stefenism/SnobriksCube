using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject playerCharacter;
    Vector3 velocity;
    Vector3 velocityRot;

    public float smoothTimeY;
    public float smoothTimeX;
    public float smoothTimeZ;

    public float smoothZoomTimeY;
    public float smoothZoomTimeX;
    public float smoothZoomTimeZ;
    public float smoothRotTimeY;
    public float smoothRotTimeX;
    float screenAddX = 0;
    float screenAddY = 0;
    float rotAddX = 0;
    float rotAddY = 0;
    float rotShiftX = 0;
    float rotShiftY = 0;
    float rotShiftZ = 0;
    public bool zoomOut = false;
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.P)){
              SceneManager.LoadScene("JordanScene",LoadSceneMode.Single);
        }

        zoomOut = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            GameManager.gameDaddy.player.GetPlayerState().setPlayerFrozen();
        }
            else   if( Input.GetKeyUp(KeyCode.LeftShift)){
            GameManager.gameDaddy.player.GetPlayerState().setPlayerControllable();
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotAddX = 0;
            rotAddY = -30;

            rotShiftX = 4;
            rotShiftY = 0;
            rotShiftZ = 2f;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotAddX = 0;
            rotAddY = 30;

            rotShiftX = -4;
            rotShiftY = 0;
            rotShiftZ = 2f;

        }
        else if (Input.GetKey(KeyCode.W))
        {
            rotAddX = 30;
            rotAddY = 0;

            rotShiftX = 0;
            rotShiftY = 4;
            rotShiftZ = 2f;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            rotAddX = -30;
            rotAddY = 0;

            rotShiftX = 0;
            rotShiftY = -4;
            rotShiftZ = 2f;
        }
        else
        {
            rotAddX = 0;
            rotAddY = 0;

            rotShiftX = 0;
            rotShiftY = 0;
            rotShiftZ = 0;

        }

        if (zoomOut)
        {
            screenAddX = (Input.mousePosition.x / Screen.width) / 2 + .75f;
            screenAddY = (Input.mousePosition.y / Screen.height) / 2 + .75f;


            //transform.eulerAngles = new Vector3(-(screenAddY-1)*5,screenAddX*5,0);
            float posX = Mathf.SmoothDamp(transform.position.x, 1.5f * screenAddX + rotShiftX, ref velocity.x, smoothZoomTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, 1.5f * screenAddY + rotShiftY, ref velocity.y, smoothZoomTimeY);
            float posZ = Mathf.SmoothDamp(transform.position.z, -5.6f + rotShiftZ, ref velocity.z, smoothZoomTimeZ);
            transform.position = new Vector3(posX, posY, posZ);

            float rotX = Mathf.SmoothDampAngle(transform.eulerAngles.x, rotAddX, ref velocityRot.x, smoothRotTimeX);
            float rotY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotAddY, ref velocityRot.y, smoothRotTimeY);
            transform.eulerAngles = new Vector3(rotX, rotY, 0);
        }
        else
        {

            float posX = Mathf.SmoothDamp(transform.position.x, playerCharacter.transform.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, playerCharacter.transform.position.y, ref velocity.y, smoothTimeY);
            float posZ = Mathf.SmoothDamp(transform.position.z, -2, ref velocity.z, smoothTimeZ);
            transform.position = new Vector3(posX, posY, posZ);

            float rotX = Mathf.SmoothDampAngle(transform.eulerAngles.x, 0, ref velocityRot.x, smoothRotTimeX);
            float rotY = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0, ref velocityRot.y, smoothRotTimeY);
            transform.eulerAngles = new Vector3(rotX, rotY, 0);
        }
    }
}
