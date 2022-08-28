using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    float currentTimer_spawnEnemy;
    [Header("Таймеры")]
    [Range(0, 20)]public float Timer_spawnEnemy;
    [Header("Компоненты")]
    public List<Vector2> spawns_enemy = new List<Vector2>();
    public GameObject enemyObject;

    private void Start()
    {
        GameObject ContainerSpawnsEnemy = GameObject.Find("SpawnsEnemy");
        for (int i = 0; i <= 3; i++)
        {
            Transform childTransform = ContainerSpawnsEnemy.transform.GetChild(i).transform;
            spawns_enemy.Add(new Vector2(childTransform.position.x, childTransform.position.y));
        }
    }
    private void Update()
    {
        CheckingTimers();
        if (currentTimer_spawnEnemy <= 0)
            SpawnEnemy();
    }
    void SpawnEnemy()
    {
        currentTimer_spawnEnemy = Timer_spawnEnemy;
        int LeftRight = Random.Range(0, 2);
        if (LeftRight == 1)
            Instantiate(enemyObject, new Vector2(spawns_enemy[0].x, Random.Range(spawns_enemy[0].y, spawns_enemy[1].y)), Quaternion.identity);
        else if (LeftRight == 0)
            Instantiate(enemyObject, new Vector2(spawns_enemy[2].x, Random.Range(spawns_enemy[2].y, spawns_enemy[3].y)), Quaternion.identity);
    }
    void CheckingTimers()
    {
        if (currentTimer_spawnEnemy > 0)
            currentTimer_spawnEnemy -= 1 * Time.deltaTime;
    }
}
