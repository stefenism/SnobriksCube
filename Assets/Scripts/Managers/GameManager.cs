using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameDaddy = null;

    public PlayerController player;
    public BlockCollisionManager blockCollision;
    public HudScript hud;

    public Block iglooBlock;
    public IglooHouse igloo;

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
        hud.gameEnd =true;
        player.GetPlayerState().setPlayerDead();
        Debug.Log("The game has now ended");
    }
    public void dead()
    {
        player.GetPlayerState().setPlayerDead();
        //StartCoroutine("playerDead");
    }

    public BlockRoom getPlayerRoom(){return player.GetPlayerState().getCurrentRoom();}
    public void setPlayerRoom(BlockRoom newRoom){player.GetPlayerState().setCurrentRoom(newRoom);}

    public void changePlayerHealth(float healthChange){player.GetPlayerHealth().addHealth(healthChange);}

    public void setIglooHouseCollisionLayer(string physicsLayer)
    {
        igloo.setIglooCollisionLayer(physicsLayer);
    }

    IEnumerator playerDead()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("player is dead");
       // Scene scene = SceneManager.GetActiveScene(); 
        //SceneManager.LoadScene(scene.name);
    }
}
