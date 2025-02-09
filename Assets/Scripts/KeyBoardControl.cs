﻿using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class KeyBoardControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Canvas/Menu").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    // Update is called once per frame
    public static bool show = false;
    public static bool open = false, close = false;
    void Update()
    {
        if (open)
        {
            GameObject.Find("Canvas/Menu").GetComponent<CanvasGroup>().alpha += Time.deltaTime * 3;
            if (GameObject.Find("Canvas/Menu").GetComponent<CanvasGroup>().alpha >= 1)
            {
                GameObject.Find("Canvas/Menu").GetComponent<CanvasGroup>().alpha = 1;
                show = true; open = false;
            }

        }
        if (close)
        {
            GameObject.Find("Canvas/Menu").GetComponent<CanvasGroup>().alpha -= Time.deltaTime * 3;
            if (GameObject.Find("Canvas/Menu").GetComponent<CanvasGroup>().alpha <= 0)
            {
                GameObject.Find("Canvas/Menu").GetComponent<CanvasGroup>().alpha = 0;
                show = false; close = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().alpha == 1)
            {
                GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().alpha = 0;
                GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().interactable = !GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().interactable;
                GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().blocksRaycasts = !GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().blocksRaycasts;
            }
            else if (show == true) close = true;
            else open = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Calculate.execute = !Calculate.execute;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Calculate.sortscore = !Calculate.sortscore;
        }
        if (Input.GetKeyDown(KeyCode.F2))//截屏系统
        {
            Rect r = new Rect(0, 0, Screen.width, Screen.height);
            Camera came = GameObject.Find("Main Camera").GetComponent<Camera>();
            RenderTexture rt = new RenderTexture((int)r.width, (int)r.height, 0);

            came.targetTexture = rt;
            came.Render();
            RenderTexture.active = rt;
            Texture2D screenShot = new Texture2D((int)r.width, (int)r.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(r, 0, 0);
            screenShot.Apply();
            came.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();

            OpenFileName openFileName = new OpenFileName();
            openFileName.structSize = Marshal.SizeOf(openFileName);
            openFileName.filter = "*.png\0*.png";
            openFileName.file = new string(new char[256]);
            openFileName.maxFile = openFileName.file.Length;
            openFileName.fileTitle = new string(new char[64]);
            openFileName.maxFileTitle = openFileName.fileTitle.Length;
            openFileName.initialDir = (UnityEngine.Application.dataPath).Replace('/', '\\');//默认路径
            openFileName.title = "保存截图";
            openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

            if (LocalDialog.GetSaveFileName(openFileName))
            {
                if (openFileName.file.Contains(".png") == false) openFileName.file += ".png";
                File.WriteAllBytes(openFileName.file, bytes);
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject.Find("Canvas/UI").GetComponent<CanvasGroup>().alpha = 1 - GameObject.Find("Canvas/UI").GetComponent<CanvasGroup>().alpha;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (GameObject.Find("Canvas/Menu").GetComponent<CanvasGroup>().alpha == 0)
            {
                GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().alpha = 1 - GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().alpha;
                GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().interactable = !GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().interactable;
                GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().blocksRaycasts = !GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().blocksRaycasts;
            }
        }
    }
}
