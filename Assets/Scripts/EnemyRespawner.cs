using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    public GameObject[] Enemies;

    float[] arrPosX = { -2f, 0f, 2f };

    [SerializeField]
    float spawnInterval = 0.5f;
    float moveSpeed = 5f;

    public Transform spawnPosition;

    int currectEnemyIndex = 0;

    int spawncount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3);

        while (true)
        {
            for (int i = 0; i < arrPosX.Length; i++)
            {
                SpawnEnemy(arrPosX[i], currectEnemyIndex, moveSpeed);
            }

            spawncount++;

            if (spawncount % 2 == 0)
            {
                currectEnemyIndex++;
                if (currectEnemyIndex >= Enemies.Length)
                {
                    currectEnemyIndex = Enemies.Length - 1;
                }
                moveSpeed += 2;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, spawnPosition.position.y, spawnPosition.position.z);

        GameObject enemyObject = Instantiate(Enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }
}
