using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Heatlhtext : MonoBehaviour
{
    public float timeToLive = 0.5f;
    public float floatSpeed = 300;
    public TMP_Text textMesh;

    public Vector3 floatDirection = new Vector3(0,1,0);
    //RectTransform rectTransform;
    Color startingColor;
    float timeElapsed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<RectTransform>()
        //rectTransform = textMesh.rectTransform;
        startingColor = textMesh.color;
    }

    public void SpawnText()
    {
        timeElapsed += Time.deltaTime;

        textMesh.rectTransform.position += floatDirection * floatSpeed * Time.deltaTime;

        textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToLive));

        if (timeElapsed > timeToLive)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frames
    //void Update()
    //{
    //    timeElapsed += Time.deltaTime;

    //    textMesh.rectTransform.position += floatDirection * floatSpeed * Time.deltaTime;

    //    textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToLive));

    //    if (timeElapsed > timeToLive)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
