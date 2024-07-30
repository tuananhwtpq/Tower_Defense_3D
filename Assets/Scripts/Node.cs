using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //Tạo 1 biến color dùng để chứa màu khi hover vào node
    public Color hoverColor;
    //Biến dùng để chứa màu bắt đầu của node
    private Color startColor;

    private GameObject turret;

    private Renderer rend;

    private void Start()
    {
        //Gán biến có kiểu Renderer = GetComponent<Renderer>()
        rend = GetComponent<Renderer>();
        //Gán màu ban đầu bằng màu node
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build here --");
            return;
        }
    }

    //Khi kích or di chuyển vào thì gán màu của node bawgf maàu hover
    private void OnMouseEnter()
    {
        //Truyền vào material.color = hoverColor;
        rend.material.color = hoverColor;
    }

    //Khi thoát di chuyê thì gán lại bằng startColor
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
