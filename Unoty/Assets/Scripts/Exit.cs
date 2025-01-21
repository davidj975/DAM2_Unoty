using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI

public class SalirJuego : MonoBehaviour
{
    public Button salirButton;  // El botón de salir

    void Start()
    {
        salirButton.onClick.AddListener(SalirAplicacion);
    }

    void SalirAplicacion()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();  // Cierra la aplicación
        // Nota: Esto no funciona en el editor de Unity, solo en una build.
    }
}
