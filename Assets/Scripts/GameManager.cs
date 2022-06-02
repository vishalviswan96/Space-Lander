using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int number_Of_Coins;
    public TextMeshProUGUI coin_Text;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }
    public void UpdateCoin()
    {
        number_Of_Coins++;
        coin_Text.text = number_Of_Coins.ToString();
    }
}
