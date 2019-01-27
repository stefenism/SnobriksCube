using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private enum PlayerState
    {
        CONTROLLABLE,
        FROZEN,
        DEAD,
    }

    private enum PlayerCubeStatus
    {
        HASCUBE,
        NOCUBE,
    }

    private PlayerState playerState = PlayerState.CONTROLLABLE;
    private PlayerCubeStatus cubeStatus = PlayerCubeStatus.NOCUBE;
    private BlockRoom currentRoom;
    private Vector2 spawnPosition;

    private PlayerController player;

    private List<KeyController> keysCollected = new List<KeyController>();
    void Awake()
    {
        spawnPosition = transform.position;
        player = GetComponent<PlayerController>();
    } 


    public void collectKey(KeyController newKey)
    {
        if(!keysCollected.Contains(newKey))
            keysCollected.Add(newKey);
    }

    public bool playerIsControllable(){return playerState == PlayerState.CONTROLLABLE;}
    public bool playerIsFrozen(){return playerState == PlayerState.FROZEN;}
    public bool playerIsDead(){return playerState == PlayerState.DEAD;}

    public void setPlayerFrozen(){playerState = PlayerState.FROZEN;}
    public void setPlayerControllable(){playerState = PlayerState.CONTROLLABLE;}
    public void setPlayerDead()
    {
        if(!playerIsDead())
            player.GetSound().playMelting();
        playerState = PlayerState.DEAD;
        GameManager.gameDaddy.endGame();
        GameManager.gameDaddy.dead();
        
    }

    public void setPlayerHasCube(){cubeStatus = PlayerCubeStatus.HASCUBE;}
    public bool playerHasCube(){return cubeStatus == PlayerCubeStatus.HASCUBE;}

    public void setCurrentRoom(BlockRoom newRoom){currentRoom = newRoom;}
    public BlockRoom getCurrentRoom(){return currentRoom;}

    public Block getCurrentBlock()
    {
        Block currentBlock = GameManager.gameDaddy.blockCollision.getBlockWithRoom(currentRoom);
        return currentBlock;
    }
}
