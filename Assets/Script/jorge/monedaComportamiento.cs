using UnityEngine;
using TMPro;

public class monedaComportamiento : MonoBehaviour
{
    //Con static, el contador no se reinicia cuando una moneda se destruye
    private static int contadorMonedasScore = 0; 
    private static int contadorPanelMonedas = 0; 
    // Es el texto del panel puntuacion (las del SCORE)
    [SerializeField] private TMP_Text textomonedas;
    // Es el texto del panel monedas
    [SerializeField] private TMP_Text textomonedasPuntosUnoCien;

    void Start()
    {
        // Me permite que aparezcan los 6 ceros en el txt
        textomonedas.text = contadorMonedasScore.ToString("D6");
        textomonedasPuntosUnoCien.text = contadorPanelMonedas.ToString("D2");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            contadorMonedasScore += 100;
            textomonedas.text = contadorMonedasScore.ToString("D6");
            Destroy(gameObject);
        }
         if (collision.CompareTag("Player"))
        {
            contadorPanelMonedas += 1;
            textomonedasPuntosUnoCien.text = contadorPanelMonedas.ToString("D2");
            Destroy(gameObject);
        }
    }
}