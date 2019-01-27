using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiksCube : MonoBehaviour
{

    void cube_collected()
    {
        GameManager.gameDaddy.player.GetPlayerState().setPlayerHasCube();
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ProjectConstants.PLAYER_TAG)
            cube_collected();
    }
}
