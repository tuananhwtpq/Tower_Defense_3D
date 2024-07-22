using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Tạo 1 biến speed dùng để kiểm soát tốc độ
    public float speed = 20f;
    //Biến target dùng để lưu vị trí waypoint
    private Transform target;
    private int wavePointIndex = 0;

    //Haàm Start được gọi sau khi gameobject được active lần đầu 1 frame
    //Chỉ được gọi sau khi tất cả hàm Awake được gọi
    private void Start()
    {
        //Ở hàm Start, gán target = waypoint đầu tiên => Tham chiêếu WayPoints.points[0]
        target = WayPoints.points[0];
    }

    private void Update()
    {
        //Tạo 1 vector3 lưu trữ khoảng cách giữa waypoint target và mục tiêu hiện tại
        Vector3 dir = target.position - transform.position;
        //normalized => đảm bảo đối tượng luôn có được 1 speed cố định
        //Time.deltaTime => đảm bảo tốc độ không phụ thuộc vào frame => không có máy nào chạy cùng frame rate
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        
        //Nếu khoảng cách giữa đối tượng và waypoint target < 0.4f thì gọi hàm GetNextWayPoint
        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            GetNextWayPoint();
        }
        
    }

    //Hàm này dùng để chuyển sang wayPoint tiếp theo
    void GetNextWayPoint()
    {
        //Nêu wavePointIndex >= số lượng wayPoint => Object sẽ bị phá hủy
        //Hiểu đơn giản là đến end waypoint rồi thì bị hủy
        if (wavePointIndex >= (WayPoints.points.Length - 1))
        {
            //Đôi lúc destroy cần có thời gian nên nó sẽ nhảy xuống đoạn code dưới => lỗi index out of range=> cần có thêm return
            Destroy(gameObject);
            return;
        }
        //Tăng wavePointIndex lên 1 đơn vị
        wavePointIndex++;
        //Gán lại target bằng vị trí next wayPoint
        target = WayPoints.points[wavePointIndex];
    }
}
