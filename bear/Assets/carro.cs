using UnityEngine;

public class CarController : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Velocidad de movimiento normal
    public float velocidadGiro = 3f; // Velocidad de giro
    public float maximaInclinacion = 30f; // Máxima inclinación permitida antes de aplicar fuerza para enderezar
    public float fuerzaEnderezamiento = 10f; // Fuerza aplicada para enderezar el carro

    private float inputHorizontal;

    void Update()
    {
        // Obtener la entrada del eje horizontal (teclas de dirección o A y D)
        inputHorizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // Obtener la entrada del eje vertical (teclas de arriba y abajo o W y S)
        float inputVertical = Input.GetAxis("Vertical");

        // Calcular la velocidad actual dependiendo de si se está presionando el botón de correr (Shift)
        float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadMovimiento * 2f : velocidadMovimiento;

        // Calcular la dirección del movimiento
        Vector3 movimiento = transform.forward * inputVertical * velocidadActual;

        // Aplicar el movimiento
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + movimiento * Time.fixedDeltaTime);

        // Calcular la rotación del carro
        float giroY = inputHorizontal * velocidadGiro;
        Quaternion rotacion = Quaternion.Euler(0f, giroY, 0f);

        // Aplicar la rotación
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * rotacion);

        // Detectar si el carro está inclinado
        if (Mathf.Abs(transform.rotation.eulerAngles.z) > maximaInclinacion)
        {
            // Calcular la fuerza de enderezamiento
            float fuerzaZ = -GetComponent<Rigidbody>().angularVelocity.z * fuerzaEnderezamiento;

            // Aplicar la fuerza de enderezamiento
            GetComponent<Rigidbody>().AddRelativeTorque(0f, 0f, fuerzaZ * Time.fixedDeltaTime);
        }
    }
}
