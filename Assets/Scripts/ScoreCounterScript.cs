using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounterScript : MonoBehaviour
{
    public GameObject[] singles;
    public GameObject[] tens;
    public GameObject[] hundreds;

    private int score = 0;

    public void SetScore(float s)
    {
        score = Convert.ToInt32(s);
        DisplayScore();
    }

    private void DisplayScore()
    {
        int single = score % 10;
        int ten = ((score % 100) - single) / 10;
        int hundred = ((score % 1000) - ten - single) / 100;

        print(single);
        print(ten);
        print(hundred);
        for (int i = 0; i < 10; i++){
            singles[i].SetActive((i <= single - 1));
            tens[i].SetActive((i <= ten - 1));
        }

        for (int j = 0; j < 9; j++)
        {
            hundreds[j].SetActive((j <= hundred - 1));
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
