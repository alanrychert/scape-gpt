using UnityEngine;

public class FloatingAvatar : RoomObject
{
    [SerializeField] public Transform objetivo; // El objeto que seguirá (el PlayerController)
    [SerializeField] public SpeechTextManager speechTextManager;
    [SerializeField] public Light avatarLight;
    public float distanciaSeguimiento = 5f; // Distancia a la que seguirá al objetivo
    public float velocidadSeguimiento = 2f; // Velocidad de seguimiento
    public float amplitudFlotacion = 1f; // Amplitud de la flotación
    public float velocidadFlotacion = 2f; // Velocidad de flotación
    public float lightRangeWhenListening=0.7f;
    public float lightRangeWhenNotListening=0.55f;

    private Vector3 posicionInicial;
    private float tiempoInicio;
    private bool isListening;

    protected override void Start(){
        base.Start();
        posicionInicial = transform.position;
        tiempoInicio = Time.time;
        isListening = false;
    }

    public override void Visited(){
        if (isListening){
            speechTextManager.StartListening();
            avatarLight.range=lightRangeWhenListening;
        }
        else{
            speechTextManager.StopListening();
            avatarLight.range=lightRangeWhenNotListening;
        }
        isListening=!isListening;
    }

    private void Update()
    {
        // Seguir al objetivo
        Vector3 direccion = objetivo.position - transform.position;
        direccion.y = 0; // No cambiamos la altura
        if (direccion.magnitude > distanciaSeguimiento)
        {
            transform.Translate(direccion.normalized * velocidadSeguimiento * Time.deltaTime);
        }

        // Flotar
        float alturaFlotacion = Mathf.Sin((Time.time - tiempoInicio) * velocidadFlotacion) * amplitudFlotacion;
        transform.position = new Vector3(transform.position.x, posicionInicial.y + alturaFlotacion, transform.position.z);
    }
}