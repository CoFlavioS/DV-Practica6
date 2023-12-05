using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TokenController : MonoBehaviour
{
    private GameController gC;
    private AudioSource aS;
    private SpriteRenderer sR;
    private CircleCollider2D cR;

    void Start()
    {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        aS = GetComponent<AudioSource>();
        sR = GetComponent<SpriteRenderer>();
        sR.enabled = true;
        cR = GetComponent <CircleCollider2D>();
        cR.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gC.CapturedToken();
        aS.Play();
        sR.enabled = false;
        cR.enabled = false;
    }
}
