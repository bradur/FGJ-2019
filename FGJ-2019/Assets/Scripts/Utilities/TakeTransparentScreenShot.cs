// Date   : 24.02.2018 09:31
// Project: snipergame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class TakeTransparentScreenShot : MonoBehaviour
{

    void Start()
    {

    }

    private bool takeShot = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("SCREEN!");
            takeShot = true;
        }
    }

    private void OnPostRender()
    {
        if (takeShot)
        {
            List<GameObject> disabledCameras = new List<GameObject>();
            foreach (GameObject cameraObject in GameObject.FindGameObjectsWithTag("MainCamera")) {
                if (cameraObject != gameObject && cameraObject.activeSelf) {
                    cameraObject.SetActive(false);
                    disabledCameras.Add(cameraObject);
                }
            }
            takeShot = false;
            GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 32);
            Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
            screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            screenShot.Apply();
            byte[] pngShot = screenShot.EncodeToPNG();
            string filename = Application.dataPath + "/" + screenShot.ToString() + "_" + Random.Range(0, 1024).ToString() + ".png";
            Debug.Log(string.Format("Writing screenshot: {0}", filename));
            File.WriteAllBytes(filename, pngShot);
            Destroy(screenShot);
            GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            foreach (GameObject disabledCamera in disabledCameras) {
                disabledCamera.SetActive(true);
            }
        }
    }
}
