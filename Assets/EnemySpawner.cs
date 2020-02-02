using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int killsGoal;
    public ObjectPool enemyPool, bulletPool;
    [SerializeField]
    private float baseSpawnTime;
    [SerializeField]
    private Transform target;

    private int currentKills, enemiesOut;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnLoop");
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(baseSpawnTime + enemiesOut * baseSpawnTime * 0.3f);

        }
    }

    private void Spawn()
    {
        LayerMask customMask = (1 << LayerMask.NameToLayer("Wall")) | (1 << LayerMask.GetMask("Bullet")) | (1 << LayerMask.GetMask("Enemy"));
        bool appeared = false;
        while (!appeared)
        {
            float enemyDistance = Random.Range(2f, 4f);
            Vector2 randomAngle = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * enemyDistance;
            if (!IsObstacleInDirection(randomAngle, enemyDistance, customMask))
            {
                appeared = true;
                GameObject enemy = enemyPool.InstantiateFromPool(target.position + (Vector3)randomAngle * enemyDistance, Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
                enemy.GetComponent<EnemyScript>().setTarget(target);
                enemy.GetComponent<EnemyScript>().setBulletPool(bulletPool);
                enemiesOut++;
            }
            else Debug.Log("Obstacle!");
        }
    }

    private bool IsObstacleInDirection(Vector2 angle, float distance, LayerMask mask)
    {
        //Debug.DrawRay(transform.position, angle, Color.green, distance);
        return Physics2D.Raycast(target.position, angle, distance, mask);
    }
}