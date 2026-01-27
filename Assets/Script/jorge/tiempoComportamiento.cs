using TMPro;
using UnityEngine;

public class tiempoComportamiento : MonoBehaviour
{
    private static float tiempoRestante = 400f;
    [SerializeField] private TMP_Text textoTiempo;

    void Start()
    {
        textoTiempo.text = ((int)tiempoRestante).ToString("D3");
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            // Me sirve para ir restando el tiempo al marcador TIME
            tiempoRestante -= Time.deltaTime;

            // Actualizamos el texto convirtiendo el float a int
            textoTiempo.text = ((int)tiempoRestante).ToString("D3");
        }
    }
}