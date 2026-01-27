using System;
using UnityEngine;

public class marioComportamiento : MonoBehaviour
{
    public float velocidad = 5f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
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