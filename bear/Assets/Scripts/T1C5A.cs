using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class T1C5A : MonoBehaviour
{
    public GameObject submarinoMotores, submarinoHUD, submarinoCamera;
    [SerializeField] RawImage[] pics;
    

    public void OpenCameraView(){
        submarinoCamera.SetActive(false);
        Invoke("TakePictureActiveButton", 2.0f); //Invoke para activar el botón para tomare la foto después de la transacción de la cámara
    }

    public void TakeAPhoto(Texture photo){
        ScreenShooter SS = gameObject.GetComponent<ScreenShooter>();
        submarinoCamera.SetActive(true);
            
        submarinoHUD.transform.GetChild(0).gameObject.SetActive(false);

        RawImage[] pantallaFotosRaw = submarinoHUD.transform.GetChild(1).GetComponentsInChildren<RawImage>(true);

        submarinoHUD.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void TakePictureActiveButton()
    {
        submarinoHUD.transform.GetChild(0).gameObject.SetActive(true);
    }
}
