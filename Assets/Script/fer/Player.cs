using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 5f;
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.position += new Vector3(0, 5, 0) * velocidad * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(1, 0, 0) * velocidad * Time.deltaTime;
        }


    }
}