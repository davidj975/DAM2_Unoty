using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI

public class LoadGame : MonoBehaviour
{
    public Button cargarButton;  // El bot�n de cargar partida

    void Start()
    {
        cargarButton.onClick.AddListener(MostrarMensaje);
    }

    void MostrarMensaje()
    {
        Debug.Log("Funcionalidad de cargar partida pendiente.");  // Muestra un mensaje en la consola
        // Aqu� podr�as agregar un popup o texto en pantalla si prefieres
    }
}
