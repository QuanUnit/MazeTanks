using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private uint scoreValue = 0;
    private void Start()
    {
        scoreText.text = scoreValue.ToString();
    }
    public void Increment()
    {
        scoreValue++;
        scoreText.text = scoreValue.ToString();
    }
    public void ResetScoreValue()
    {
        scoreValue = 0;
    }
}
