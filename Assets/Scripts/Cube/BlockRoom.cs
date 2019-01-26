using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BlockRoom : MonoBehaviour
{
    public enum RoomType
    {
        WINTER,
        SPRING,
        FALL,
        SUMMER,
        LAVA,
        DROUGHT,
    }

    public RoomType roomType = RoomType.WINTER; 

    [Range(-100, 100)]
    public float healthModifier = 0;
    [Range(0, 10)]
    public float timeToModifyHealth = 0;
    private float currentTime = 0;

    public void Update()
    {
        affect_player_health();
    }
    private void affect_player_health()
    {
        if(GameManager.gameDaddy.getPlayerRoom() == this)
        {
            if(currentTime >= timeToModifyHealth)
            {
                currentTime = 0;
                GameManager.gameDaddy.changePlayerHealth(healthModifier);
            }
            currentTime += Time.fixedDeltaTime;
        }
    }

    private void reset_room()
    {
        currentTime = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ProjectConstants.PLAYER_TAG)
        {
            GameManager.gameDaddy.setPlayerRoom(this);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ProjectConstants.PLAYER_TAG)
        {
            reset_room();
        }
    }
}
