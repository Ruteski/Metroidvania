using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    
    public int health;
    public int heartsCount;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite noHeart;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++) {
            //controla a vida 
            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = noHeart;
            }

            // mostra a quantidade total de imagens da vida que o player vai ter
            if (i < heartsCount) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }
}
