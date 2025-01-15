using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardLogic : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 offset; // Diferencia entre el objeto y el rat�n al iniciar el arrastre
    private Vector3 startPosition; // Posici�n inicial del objeto
    private Camera mainCamera; // Referencia a la c�mara principal

    private Vector3 lastPosition; // �ltima posici�n conocida para calcular la direcci�n de movimiento
    private Quaternion initialRotation; // Rotaci�n inicial de la carta

    [Header("Lerp Settings")]
    public float lerpSpeed = 10f; // Velocidad del Lerp

    [Header("Rotation Settings")]
    public float rotationAmount = 20f; // Cantidad de rotaci�n basada en el movimiento
    public float rotationSpeed = 20f; // Velocidad de rotaci�n de la carta

    [Header("Scale Settings")]
    public float scaleOnHover = 1.15f;
    public float scaleOnSelect = 1.25f;
    public float scaleTransition = 0.15f;
    public Ease scaleEase = Ease.OutBack;

    private bool isReturning; // Indica si el objeto est� volviendo a la posici�n inicial
    private bool isDragging; // Indica si el objeto est� siendo arrastrado

    private Transform tiltParent; // Transform para manejar la inclinaci�n

    void Start()
    {
        // Guardamos la posici�n inicial del objeto
        startPosition = transform.position;

        // Obtenemos la c�mara principal
        mainCamera = Camera.main;

        // Iniciamos la �ltima posici�n
        lastPosition = transform.position;

        // Guardamos la rotaci�n inicial
        initialRotation = transform.rotation;

        // Crear un tiltParent si no existe
        tiltParent = new GameObject("TiltParent").transform;
        tiltParent.parent = transform.parent;
        tiltParent.position = transform.position;
        tiltParent.rotation = transform.rotation;

        // Asegurar que las escalas iniciales sean consistentes
        tiltParent.localScale = Vector3.one;
        transform.localScale = Vector3.one;

        // Reasignar el transform como hijo del tiltParent
        transform.parent = tiltParent;
    }

    void Update()
    {
        // Si el objeto est� regresando, hacer el Lerp hacia la posici�n inicial
        if (isReturning)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, lerpSpeed * Time.deltaTime);
            tiltParent.rotation = Quaternion.Lerp(tiltParent.rotation, initialRotation, rotationSpeed * Time.deltaTime);

            // Si est� suficientemente cerca de la posici�n inicial, detener el Lerp
            if (Vector3.Distance(transform.position, startPosition) < 0.01f && Quaternion.Angle(tiltParent.rotation, initialRotation) < 0.5f)
            {
                transform.position = startPosition; // Asegura que se llega exactamente a la posici�n inicial
                tiltParent.rotation = initialRotation; // Resetea la rotaci�n
                isReturning = false;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Convertimos la posici�n del rat�n en coordenadas del mundo
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Calculamos el offset entre el objeto y el rat�n
        offset = mouseWorldPosition - transform.position;
        offset.z = 0; // Aseguramos que el eje Z no se vea afectado

        isDragging = true; // El arrastre ha comenzado
        isReturning = false; // Aseguramos que no vuelva si est� siendo arrastrado

        // Guardamos la posici�n inicial para calcular la direcci�n de movimiento
        lastPosition = transform.position;

        // Escalar directamente la carta
        transform.DOScale(scaleOnSelect, scaleTransition).SetEase(scaleEase);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Si el objeto est� siendo arrastrado, seguimos movi�ndolo
        if (isDragging)
        {
            // Convertimos la posici�n del rat�n en coordenadas del mundo
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0; // Aseguramos que no afectamos el eje Z

            // Ajustamos la posici�n del objeto con el offset
            transform.position = mouseWorldPosition - offset;

            // Calculamos la velocidad de movimiento
            Vector3 movementDelta = transform.position - lastPosition;

            // Calculamos el �ngulo de rotaci�n basado en la velocidad
            float rotationAngle = Mathf.Clamp(movementDelta.x * rotationAmount, -rotationAmount, rotationAmount);

            // Aplicamos la rotaci�n suavemente
            Quaternion targetRotation = Quaternion.Euler(0, 0, -rotationAngle);
            tiltParent.rotation = Quaternion.Lerp(tiltParent.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Actualizamos la �ltima posici�n
            lastPosition = transform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // El arrastre ha terminado, permitimos que el objeto vuelva a su posici�n inicial
        isDragging = false;
        isReturning = true; // Iniciar el retorno a la posici�n inicial

        // Escalar de vuelta al tama�o original
        transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }

    private void OnMouseEnter()
    {
        // Escalar directamente la carta al pasar el cursor por encima
        transform.DOScale(scaleOnHover, scaleTransition).SetEase(scaleEase);
    }

    private void OnMouseExit()
    {
        // Escalar de vuelta al tama�o original al salir el cursor
        transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }
}