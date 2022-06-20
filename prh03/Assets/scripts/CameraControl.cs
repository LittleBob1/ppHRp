using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject followAt;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(followAt.transform.position.x, followAt.transform.position.y, -10);
    }
}
