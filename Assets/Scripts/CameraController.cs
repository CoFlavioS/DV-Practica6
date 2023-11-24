using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void MoveCamera(Vector3 playerPos)
    {
        int camX = Mathf.FloorToInt(playerPos.x / 35);
        int camY = Mathf.FloorToInt(playerPos.y / 20);

        transform.position = new Vector3(17.5f + camX * 35, 10 + camY * 20, -10);
    }
}
