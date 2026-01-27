
using UnityEngine;

public class plataformaComportamiento : MonoBehaviour
{
   public Transform puntoInicio;
   public Transform puntoFinal;
   public float velocidad;

   public bool invertidoMov;

   Vector3 posicionObjetivo;
   Vector3 puntoIn;
   Vector3 puntoFin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        puntoIn = puntoInicio.position;
        puntoFin = puntoFinal.position;
        posicionObjetivo = puntoFin;

    }

    // Update is called once per frame
    void Update()
    {
        float velocidadFija = velocidad*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,posicionObjetivo, velocidadFija);
        if(transform.position == posicionObjetivo)
        {
            if(invertidoMov)
            {
                if(posicionObjetivo == puntoIn)
                {
                    posicionObjetivo = puntoFin;
                }
                else
                {
                    posicionObjetivo = puntoIn;
                }
            }
            else
                {
                  transform.position = puntoIn;  
                }
            
        }
    }
}