﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class camera_move : MonoBehaviour
{
    //public Transform target=new Transform();
    public static double a, b, distance;
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 vec = new Vector3(0, 0, 0);
        //target.position = vec;
        //a =Math.PI; b = Math.PI / 2 - 0.1; 
        distance = 200;
        setplace((float)Math.Cos(a) * (float)Math.Cos(b) * (float)distance, (float)Math.Sin(b) * (float)distance, (float)Math.Sin(a) * (float)Math.Cos(b) * (float)distance);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, 0, 0) - transform.position), 10);
    }
    public void setplace(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
    Vector2 now = new Vector2();
    // Update is called once per frame
    void Update()
    {
        if(KeyBoardControl.show) return;
        if(GameObject.Find("Canvas/Setting").GetComponent<CanvasGroup>().alpha == 1) return;
        if (Input.GetMouseButtonDown(0))
        {
            now = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 new1 = Input.mousePosition;
            a -= (new1.x - now.x) * 0.01;
            b -= (new1.y - now.y) * 0.004;
            if (b <= -Math.PI / 2) b = -Math.PI / 2 + 0.04;
            if (b >= Math.PI / 2) b = Math.PI / 2 - 0.04;
            setplace((float)Math.Cos(a) * (float)Math.Cos(b) * (float)distance, (float)Math.Sin(b) * (float)distance, (float)Math.Sin(a) * (float)Math.Cos(b) * (float)distance);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(0, 0, 0) - transform.position), 10);
            now = new1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            distance -= Input.mouseScrollDelta.y * 50;
            if (distance < 50) distance = 50;
            setplace((float)Math.Cos(a) * (float)Math.Cos(b) * (float)distance, (float)Math.Sin(b) * (float)distance, (float)Math.Sin(a) * (float)Math.Cos(b) * (float)distance);
        }
    }
}
