using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    private ParticleSystem particle;
    private LineRenderer line;

    const float duration = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        line = GetComponentInChildren<LineRenderer>();
    }

    public void StartEmit(float lineDistance)
    {
        particle.Play();
        Vector3 endPos = transform.position + transform.forward * lineDistance;
        DrawLine(endPos);
    }

    public void StartEmit(Vector3 endPos)
    {
        particle.Play();
        DrawLine(endPos);
    }

    void DrawLine(Vector3 endPos) 
    {
        line.enabled = true;
        Vector3[] points = new Vector3[2];
        points[0] = transform.position;
        points[1] = endPos;
        line.positionCount = points.Length;
        line.SetPositions(points);

        Invoke("StopEmit", duration);
    }    

    void StopEmit() 
    {
        line.enabled = false;
    }        
}