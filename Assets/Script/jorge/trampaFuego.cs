using UnityEngine;

public class trampaFuego : MonoBehaviour
{
    public float velocidadRotacion = 75;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // forward me permite el giro estilo hélice junto con el rotate
       transform.Rotate(Vector3.forward*velocidadRotacion*Time.deltaTime); 
    }
}
