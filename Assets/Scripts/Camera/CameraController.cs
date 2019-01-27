using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerCharacter;
    Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;
    // Update is called once per frame
    void Update()
    {
        //transform.position = playerCharacter.transform.position+new Vector3(0,0,-1);
        float posX = Mathf.SmoothDamp(transform.position.x,playerCharacter.transform.position.x,ref velocity.x,smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y,playerCharacter.transform.position.y,ref velocity.y,smoothTimeY);
        transform.position = new Vector3(posX,posY,-2);
    }
}
