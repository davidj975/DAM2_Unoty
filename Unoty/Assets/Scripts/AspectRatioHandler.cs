using UnityEngine;

public class AspectRatioHandler : MonoBehaviour
{
    void Start()
    {
        // Establecer la relaci�n de aspecto deseada
        float targetAspect = 16f / 9f;

        // Obtener la relaci�n de aspecto actual de la pantalla
        float currentAspect = (float)Screen.width / (float)Screen.height;

        // Calcular la diferencia de aspecto
        float scaleHeight = currentAspect / targetAspect;

        // Ajustar la c�mara
        Camera camera = Camera.main;
        if (scaleHeight < 1.0f)
        {
            // Si la relaci�n de aspecto es m�s alta que la deseada, rellena el lado
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            // Si la relaci�n de aspecto es m�s baja que la deseada, rellena arriba y abajo
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
