using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera camaraNormal;
    public Camera camaraFotos;
    public LayerMask capasObjetos;
    public float sensibilidadX = 2f;
    public float sensibilidadY = 2f;
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;
    public GameObject prefabFotoTomada;
    public Texture2D textureToApply; // Textura que deseas aplicar al cubo

    private float rotacionX = 0f;
    private float rotacionY = 0f;
    private RaycastHit hit;

    void Start()
    {
        if (camaraFotos != null)
        {
            camaraFotos.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CambiarCamara();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CapturarFoto();
        }
    }

    void CambiarCamara()
    {
        if (camaraNormal != null && camaraFotos != null)
        {
            camaraNormal.enabled = !camaraNormal.enabled;
            camaraFotos.enabled = !camaraFotos.enabled;

            if (camaraFotos.enabled)
            {
                Vector3 rotacionActual = transform.eulerAngles;
                rotacionX = rotacionActual.x;
                rotacionY = rotacionActual.y;
            }
        }
    }

    void CapturarFoto()
    {
        if (camaraFotos != null && camaraFotos.enabled)
        {
            Ray rayo = camaraFotos.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(rayo.origin, rayo.direction * 100f, Color.green);

            if (Physics.Raycast(rayo, out hit, Mathf.Infinity, capasObjetos))
            {
                Debug.Log("Objeto detectado: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject.name == "capsule")
                {
                    // Obtener el renderer del cubo
                    Renderer cubeRenderer = prefabFotoTomada.GetComponent<Renderer>();

                    // Crear un nuevo material y asignarle la textura deseada
                    Material newMaterial = new Material(Shader.Find("Standard"));
                    newMaterial.mainTexture = textureToApply;

                    // Aplicar el nuevo material al cubo
                    cubeRenderer.material = newMaterial;

                    // Activar el prefabFotoTomada en la posición del objeto detectado
                    prefabFotoTomada.SetActive(true);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (camaraFotos != null && camaraFotos.enabled)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensibilidadX;
            float mouseY = Input.GetAxis("Mouse Y") * sensibilidadY;

            rotacionY += mouseX;
            rotacionX -= mouseY;

            rotacionX = Mathf.Clamp(rotacionX, minVerticalAngle, maxVerticalAngle);

            transform.rotation = Quaternion.Euler(rotacionX, rotacionY, 0);
        }
    }
}
