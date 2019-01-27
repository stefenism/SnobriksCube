using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameDaddy = null;

    public PlayerController player;
    public BlockCollisionManager blockCollision;

    Transform currentPlayerSpawn;

    void Awake()
    {
        if(gameDaddy == null)
        {
            gameDaddy = this;
        }

        else if(gameDaddy == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad (gameObject);

        if(blockCollision != null)
            blockCollision.Initialize();

    }

    public void endGame()
    {
        player.GetPlayerState().setPlayerFrozen();
        Debug.Log("The game has now ended");
    }

    public BlockRoom getPlayerRoom(){return player.GetPlayerState().getCurrentRoom();}
    public void setPlayerRoom(BlockRoom newRoom){player.GetPlayerState().setCurrentRoom(newRoom);}

    public void changePlayerHealth(float healthChange){player.GetPlayerHealth().addHealth(healthChange);}
}
