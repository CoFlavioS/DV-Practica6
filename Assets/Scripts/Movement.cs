using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [Range (0.1f, 1)] public float speed;

    float phase;
    float phaseDirection;

    private GameController gC;

    private void Start()
    {
        if (transform.tag == "Enemy")
            startPos = transform.parent.position;
        else
            startPos = transform.position;
        phase = 0;
        phaseDirection = 1;
        gC = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(startPos, endPos, phase);
        if (gC.move)
        {
            phase += Time.deltaTime * speed * phaseDirection;
            if (phase >= 1) phaseDirection = -1;
            else if (phase <= 0) phaseDirection = 1;
        }
    }
}
