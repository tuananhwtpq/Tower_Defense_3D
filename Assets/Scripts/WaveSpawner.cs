using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //EnemyPrefab dùng để đưa prefab của enemy vào để điều khiển
    public Transform enemyPrefab;

    //Lưu địa điểm spawn
    public Transform spawnPoint;
    //Thời gian giữa mỗi wave
    public float timeBetweenWave = 5f;
    //Đếm ngược bắt đầu chạy wave đầu
    private float countdown = 2f;

    public Text WaveCountDownText;

    //Đếm số waveNumber
    private int waveIndex = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
        }
        
        //Countdown sẽ bị -1 second mỗi frame
        countdown -= Time.deltaTime;
        WaveCountDownText.text = Mathf.Round(countdown).ToString();
    }

    //IEnumerator có thể dừng đoạn code này lại nên cta có thể đợi 1 khoảng thời gian trước khi tiếp tục chạy
    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        
    }

    //Hàm này dùng để spawn Enemy
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
