using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text myText;

    private void Update()
    {
        if (!PlayerController.died)
        {
            string a = PlayerController.coins.ToString();
            myText.text = (a);
        }
    }
}
