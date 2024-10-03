using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI ScoreText;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Score: 0" ;
    }

    public void AddScore(int points)
    {
        score += points;
        ScoreText.text = "Score: " + score;
    }

}
