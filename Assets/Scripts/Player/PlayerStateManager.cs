using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private enum PlayerState
    {
        CONTROLLABLE,
        ALIVE,
        DEAD,
    }

    private PlayerState playerState = PlayerState.CONTROLLABLE;
    private BlockRoom currentRoom;
    private Vector2 spawnPosition;

    void Awake()
    {
        spawnPosition = transform.position;
    } 

    public bool playerIsControllable(){return playerState == PlayerState.CONTROLLABLE;}
    public bool playerIsAlive(){return playerState == PlayerState.ALIVE;}
    public bool playerIsDead(){return playerState == PlayerState.DEAD;}

    public void setCurrentRoom(BlockRoom newRoom){currentRoom = newRoom;}
    public BlockRoom getCurrentRoom(){return currentRoom;}
}
