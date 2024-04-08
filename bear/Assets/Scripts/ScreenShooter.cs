using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ScreenShooter : MonoBehaviour
{
    [Tooltip("Ultima fotografía tomada")]
    public Texture LastPic {get => _lastTexture; set{ _lastTexture = value; }}
    [Tooltip("Rollo de todas las fotografias tomadas")]
    public List<Texture> CameraRoll;    
    [Tooltip("Use una funcion que reciba un Texture para recibir la fotografía y aplicarla al UI")]
    [SerializeField] UnityEvent<Texture> OnTakePic;
    public GameObject ultimaFoto;

    Texture _lastTexture;

    public GameObject cameraHUD, mainHUD, mainCamera, pictureCamera;

    public GameObject[] fotosTomadas;
    public GameObject[] fotosFinales;
    public int fotosIndex = 0;

    public LayerMask capasObjetos;
    private RaycastHit hit;

    public GameObject canvasFinal;
    public GameObject player;

    private void Update()
    {
        Ray rayo = pictureCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayo, out hit, Mathf.Infinity, capasObjetos))
        {
            if (Input.GetMouseButtonDown(0))
            {
                TakePic();
                hit.collider.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    IEnumerator RecordFrame()
    {
        yield return new WaitForEndOfFrame(); //esperamos el final del frame para asegurarse que esté dibujado todo en pantalla
        var texture = ScreenCapture.CaptureScreenshotAsTexture(); //capturamos la pantalla como textura
        LastPic = texture; //almacenamos la textura como última fotografía
        CameraRoll.Add(LastPic); //añadimos la foto al rollo
        OnTakePic?.Invoke(LastPic); //enviamos la foto tomada por el evento
        
        ultimaFoto.SetActive(true);
        ultimaFoto.GetComponent<RawImage>().texture = LastPic;

        GuardarFoto();

        StartCoroutine(ApagarCamara());
    }

    IEnumerator ApagarCamara()
    {
        yield return new WaitForSeconds(2f);

        mainCamera.SetActive(true);

        cameraHUD.SetActive(false);
        pictureCamera.SetActive(false);

        ultimaFoto.SetActive(false);
        mainHUD.SetActive(true);

        if (fotosIndex == 6)
        {
            player.GetComponent<CarController>().enabled = false;
            canvasFinal.SetActive(true);
            for(int i = 0; i < CameraRoll.Count; i++)
            {
                fotosFinales[i].GetComponent<RawImage>().texture = CameraRoll[i];
            }
        }
    }

    /// <summary>
    /// Toma un screenshoot y lo devuelve por el evento OnTakePic
    /// </summary>
    public void TakePic()
    {
        StartCoroutine(RecordFrame());
    }

    public void GuardarFoto()
    {
        fotosTomadas[fotosIndex].GetComponent<RawImage>().texture = CameraRoll[CameraRoll.Count - 1];
        fotosIndex++;
    }
}
