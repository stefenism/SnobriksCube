using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public DoorObject door;
    public Sprite keyImage;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public Sprite showKeyCollected()
    {
        return keyImage;
    }

    void do_collect_key()
    {
        GameManager.gameDaddy.player.GetSound().playKeysound();
        Debug.Log("made it");
        //door.open();
        GameManager.gameDaddy.player.GetPlayerState().collectKey(this);
        //makeKeyAppearInUI
        gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision with (key): " + collision.gameObject.name);
        if(collision.gameObject.tag == ProjectConstants.PLAYER_TAG)
            do_collect_key();

    }
}
