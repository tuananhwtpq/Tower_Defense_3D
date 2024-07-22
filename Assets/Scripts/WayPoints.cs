using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    //Tạo 1 mảng kiểu Transform dùng để lưu các way points
    //Static dùng để truy cập được mọi nơi, vd như ở lớp khác
    public static Transform[] points;

    //Hàm Awake được gọi khi Scene được load bởi Unity và đựợc gọi 1 lần cho mỗi gameObject
    private void Awake()
    {
        //Kích thước của mảng bằng transform.childCounnt => Tự đếm số lượng waypoint có trong Waypoints
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            //Gán từng phần tử của mảng points bằng transform.getChild(i) => các waypoint
            points[i] = transform.GetChild(i);
        }
        
    }
}
