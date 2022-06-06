using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsReturn : MonoBehaviour
{
    public int rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = -3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
