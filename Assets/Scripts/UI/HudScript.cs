using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudScript : MonoBehaviour
{
    public GameObject logo;
    public GameObject healthBarUI;
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    GameObject currentKey;
    public Sprite testSprite;
    int keyCount = 0;

    bool shiftVisable = false;
    float shiftAlpha = 0;
    public GameObject shiftUI;
    public CameraController camera;

    public float healthTotal = 0;
    float fade = 700;
    public bool gameEnd = false;

    public GameObject winUI;
    float winAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LogoFade();
        UpdateHealthBar();
        UpdateShift();
        UpdateEndscreen();

    }

    void LogoFade()
    {
        if (fade < 255 & fade >= 0)
        {
            logo.GetComponent<Image>().color = new Color32(255, 255, 225, (byte)fade);

        }
        fade = fade - 1.5f;
    }

    void UpdateHealthBar()
    {
        healthTotal=100-GameManager.gameDaddy.player.GetPlayerHealth().getCurrentHealth();

        healthBarUI.transform.localScale = new Vector3(1, 1 + healthTotal * 3.6f, 1);


    }
    public void AddKey(Sprite sprt)
    {
        switch (keyCount)
        {
            case 0:
                currentKey = key1;
                break;
            case 1:
                currentKey = key2;
                break;
            case 2:
                currentKey = key3;
                break;
            default:
                print("error to many keys");
                return;
        }
        keyCount++;
        currentKey.GetComponent<Image>().enabled = true;
        currentKey.GetComponent<Image>().sprite = sprt;

    }
    public void ShowShift()
    {
        shiftVisable = true;
    }

    void UpdateShift()
    {

        if (shiftAlpha < 255 & shiftAlpha >= 0)
        {
            shiftUI.GetComponent<Image>().color = new Color32(255, 255, 225, (byte)shiftAlpha);

        }
        if (shiftVisable)
        {
            if (shiftAlpha < 255)
                shiftAlpha = shiftAlpha + 1.5f;
        }
        else
        {
            if (shiftAlpha > 0)
                shiftAlpha = shiftAlpha - 1.5f;
        }
        if (camera.zoomOut)
        {
            shiftVisable = false;
        }

    }
    void UpdateEndscreen()
    {
        if (gameEnd)
        {

            if (winAlpha < 255)
            {
                winUI.GetComponent<Image>().color = new Color32(255, 255, 225, (byte)winAlpha);

            }
            winAlpha += 0.8f;
            if (winAlpha > 550){
                print("quit");
                Application.Quit();
            }
                
        }
    }
}
