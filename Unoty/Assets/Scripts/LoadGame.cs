using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI

public class LoadGame : MonoBehaviour
{
    public Button cargarButton;  // El botón de cargar partida

    void Start()
    {
        cargarButton.onClick.AddListener(MostrarMensaje);
    }

    void MostrarMensaje()
    {
        Debug.Log("Funcionalidad de cargar partida pendiente.");  // Muestra un mensaje en la consola
        // Aquí podrías agregar un popup o texto en pantalla si prefieres
    }
}
