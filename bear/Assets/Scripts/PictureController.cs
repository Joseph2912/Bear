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

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
            OpenCameraView();
    }

    public void OpenCameraView(){
        mainCamera.SetActive(false);
        pictureCamera.SetActive(true);
        cameraHUD.SetActive(true);
        mainHUD.SetActive(false);
    }

    //public void TakeAPhoto(Texture photo){
    //    ScreenShooter SS = gameObject.GetComponent<ScreenShooter>();
    //    mainCamera.SetActive(true);

    //    cameraHUD.SetActive(false);
    //    pictureCamera.SetActive(false);

    //}
}
