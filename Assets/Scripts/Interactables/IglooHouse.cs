using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IglooHouse : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ProjectConstants.PLAYER_TAG)
        {
            GameManager.gameDaddy.endGame();
        }
    }
}
