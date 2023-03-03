using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Heatlhtext : MonoBehaviour
{
    public float timeToLive = 0.5f;
    public float floatSpeed = 300;
    public TMP_Text textMesh;

    public static string dmgText { get; set; }
    public TMP_Text TextMesh { get => textMesh; set => textMesh = value; }

    public Vector3 floatDirection = new Vector3(0, 1, 0);
    //RectTransform rectTransform;
    Color startingColor;
    float timeElapsed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = dmgText.ToString();
        //textMesh.text = 
        //GetComponent<RectTransform>()
        //rectTransform = textMesh.rectTransform;
        startingColor = textMesh.color;
    }

    public void SpawnText(/*float dmg*/)
    {
        //textMesh.text = dmg.ToString();
        timeElapsed += Time.deltaTime;

        textMesh.rectTransform.position += floatSpeed * Time.deltaTime * floatDirection; // floatDirection * floatSpeed * Time.deltaTime -> default

        textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToLive));

        if (timeElapsed > timeToLive)
        {
            Destroy(this);
        }
    }

    // Update is called once per frames
    void Update()
    {
        timeElapsed += Time.deltaTime;

        textMesh.rectTransform.position += floatSpeed * Time.deltaTime * floatDirection; // floatDirection * floatSpeed * Time.deltaTime -> default

        textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToLive));

        if (timeElapsed > timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
