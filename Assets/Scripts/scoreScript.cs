using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour
{
    public static int Score;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        Score = 0;
    }

    private void Update()
    {
        text.text = Score.ToString();
    }
}
