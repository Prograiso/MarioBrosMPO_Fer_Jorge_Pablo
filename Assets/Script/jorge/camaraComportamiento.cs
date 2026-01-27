using UnityEngine;

public class comportamientoCamara : MonoBehaviour
{
    [SerializeField] private Transform target; 
    
    // Distancia y posición relativa de la cámara con respecto al objetivo
    private Vector3 posicionDeseada = new Vector3(0, 1, -10); 

    void LateUpdate()
    {
        // Establece la posición de la cámara
        // Posición del objetivo + la distancia deseada (offset)
        transform.position = target.position + posicionDeseada;
    }
}
