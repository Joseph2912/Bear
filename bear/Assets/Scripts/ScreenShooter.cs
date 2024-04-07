using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ScreenShooter : MonoBehaviour
{
    [Tooltip("Ultima fotografía tomada")]
    public Texture LastPic {get => _lastTexture; set{ _lastTexture = value; }}
    [Tooltip("Rollo de todas las fotografias tomadas")]
    public List<Texture> CameraRoll;    
    [Tooltip("Use una funcion que reciba un Texture para recibir la fotografía y aplicarla al UI")]
    [SerializeField] UnityEvent<Texture> OnTakePic;
    public UnityEvent BeforeShooting, AfterShooting;

    Texture _lastTexture;
    AudioSource AS;

    private void Start() {
        AS = gameObject.GetComponent<AudioSource>();
        AS.playOnAwake = false;
        AS.clip = (AudioClip)Resources.Load("Audios/sFx/CanonShoot");
    }
    
    IEnumerator RecordFrame()
    {
        yield return new WaitForEndOfFrame(); //esperamos el final del frame para asegurarse que esté dibujado todo en pantalla
        var texture = ScreenCapture.CaptureScreenshotAsTexture(); //capturamos la pantalla como textura
        LastPic = texture; //almacenamos la textura como última fotografía
        CameraRoll.Add(LastPic); //añadimos la foto al rollo
        OnTakePic?.Invoke(LastPic); //enviamos la foto tomada por el evento
        AfterShooting?.Invoke();
    }

    /// <summary>
    /// Toma un screenshoot y lo devuelve por el evento OnTakePic
    /// </summary>
    public void TakePic()
    {
        BeforeShooting?.Invoke();
        AS.Play();
        StartCoroutine(RecordFrame());
    }

    public void DeletePicInIndex(int index){
        CameraRoll.RemoveAt(index);
    }
}
