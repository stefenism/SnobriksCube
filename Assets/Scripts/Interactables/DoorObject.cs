using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    
    public void open()
    {
        this.gameObject.SetActive(false);
    }
}
