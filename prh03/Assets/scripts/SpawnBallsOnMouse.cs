using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallsOnMouse : MonoBehaviour
{
    public GameObject ball;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(ball,new Vector3(mousePos.x, mousePos.y, 0), Quaternion.Euler(0,0,0));
        }
    }
}
