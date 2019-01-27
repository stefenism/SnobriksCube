using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudScript : MonoBehaviour
{
    public GameObject logo;
    public GameObject healthBarUI;
    public float healthTotal = 0;
    float fade = 700;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LogoFade();
        UpdateHealthBar();
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

        healthBarUI.transform.localScale = new Vector3(1, 1 + healthTotal * 3.6f, 1);


    }
}
