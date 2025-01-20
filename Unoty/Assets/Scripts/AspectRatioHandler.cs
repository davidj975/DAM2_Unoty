using UnityEngine;

public class AspectRatioHandler : MonoBehaviour
{
    void Start()
    {
        // Establecer la relación de aspecto deseada
        float targetAspect = 16f / 9f;

        // Obtener la relación de aspecto actual de la pantalla
        float currentAspect = (float)Screen.width / (float)Screen.height;

        // Calcular la diferencia de aspecto
        float scaleHeight = currentAspect / targetAspect;

        // Ajustar la cámara
        Camera camera = Camera.main;
        if (scaleHeight < 1.0f)
        {
            // Si la relación de aspecto es más alta que la deseada, rellena el lado
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            // Si la relación de aspecto es más baja que la deseada, rellena arriba y abajo
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
    }
}
