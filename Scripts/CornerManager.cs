using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerManager : MonoBehaviour
{
    private Color[] colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, Color.white, Color.grey };
    private void Start()
    {
        InvokeRepeating("generateRandomColor", 5f, 5f);
    }
    void generateRandomColor()
    {
        Color randomColor = colors[Random.Range(0, colors.Length)];
        GetComponent<SpriteRenderer>().color = randomColor;
    }
}
