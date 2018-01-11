using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    #region Instance

    public static EnemySpawner Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (EnemySpawner)FindObjectOfType(typeof(EnemySpawner));
            }
            return instance;
        }
    }

    private static EnemySpawner instance = null;

    #endregion

    [SerializeField]
    private List<Enemy> enemyObjects;

    public IEnumerable<Enemy> SpawnEnemy(int numberOfEnemies, EnemyType enemyType, Vector3 position, Quaternion rotation)
    {
        for(int index = 0; index < numberOfEnemies; index++)
        {
            var enemy = Instantiate(enemyObjects[(int)enemyType], position, rotation);
            yield return enemy;
        }
    }
}

public class EnemyList : IEnumerable<Enemy>
{
    private List<Enemy> enemies;

    public EnemyList()
    {
        enemies = new List<Enemy>();
    }

    public void AddNewEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()    
    {
        return (enemies as IEnumerable).GetEnumerator();
    }

    public IEnumerator<Enemy> GetEnumerator()
    {
        return enemies.GetEnumerator();
    }

    #endregion
}
