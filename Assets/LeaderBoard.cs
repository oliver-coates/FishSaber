using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    public TextMeshPro text;

    private List<int> scores;

    private void Start()
    {
        scores = new List<int>();

        UpdateScores();
    }

    public void AddScore(int score)
    {
        scores.Add(score);
        UpdateScores();
    }

    void UpdateScores()
    {
        scores.Sort();
        scores.Reverse();

        int iteration = 0;

        string scoresText = "";

        foreach(int score in scores)
        {
            if (iteration == 3) {break;}

            scoresText += "\n > " + score.ToString();

            iteration += 1;
        }

        text.text = "Scoreboard:" + scoresText;
    }
}
