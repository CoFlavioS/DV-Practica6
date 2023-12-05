using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private GameController gC;

    private void Start()
    {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gC.GetSpawnpoint().GetComponent<SpriteRenderer>().color = Color.gray;
            gC.SetSpawnpoint(this.gameObject);
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
