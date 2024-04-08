using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PictureController : MonoBehaviour
{
    public GameObject cameraHUD, mainHUD, mainCamera, pictureCamera;
    public bool isCameraOn = false;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
            OpenCameraView();
    }

    public void OpenCameraView(){
        if (isCameraOn)
        {
            mainCamera.SetActive(true);
            pictureCamera.SetActive(false);
            cameraHUD.SetActive(false);
            mainHUD.SetActive(true);
            isCameraOn = !isCameraOn;
        }
        else
        {
            mainCamera.SetActive(false);
            pictureCamera.SetActive(true);
            cameraHUD.SetActive(true);
            mainHUD.SetActive(false);
            isCameraOn = !isCameraOn;
        }
        
    }

    //public void TakeAPhoto(Texture photo){
    //    ScreenShooter SS = gameObject.GetComponent<ScreenShooter>();
    //    mainCamera.SetActive(true);

    //    cameraHUD.SetActive(false);
    //    pictureCamera.SetActive(false);

    //}
}
