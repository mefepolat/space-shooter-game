using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyFab;
    private float minX = -6f;
    private float maxX = 12f;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _tripleShotFab;

    [SerializeField]
    private GameObject _tripleShotContainer;

    [SerializeField]
    private GameObject _speedFab;

    [SerializeField]
    private GameObject _shieldFab;

    private bool _stopSpawning = false;

    void Start()
    {
        
      
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        SpawnPowerUpRoutine();
    }

    void SpawnPowerUpRoutine()
    {
        StartCoroutine(TripleShotSpawnRoutine());
        StartCoroutine(SpeedPowerUpRoutine());
        StartCoroutine(ShieldPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    // spawn game objects every 5 seconds

    float XGenerator()
    {
        float x = Random.Range(minX, maxX);

        return x;
    }

    public void ReSpawnEnemy()
    {
        float x = XGenerator();
       
      GameObject newEnemy = Instantiate(_enemyFab, new Vector3(x, 7, 0), Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    public void ReSpawnTripleShotPowerUp()
    {
        float x = XGenerator();

        GameObject newPowerUp = Instantiate(_tripleShotFab, new Vector3(x, 7, 0), Quaternion.identity);
        newPowerUp.transform.parent = _tripleShotContainer.transform;
    }

    public void ReSpawnSpeedPowerUp()
    {
        float x = XGenerator();
        GameObject newPowerUp = Instantiate(_speedFab, new Vector3(x, 7, 0), Quaternion.identity);
    }

    public void ReSpawnShieldPowerUp()
    {
        
        float x = XGenerator();
        GameObject newPowerUp = Instantiate(_shieldFab, new Vector3(x, 7, 0), Quaternion.identity);
        newPowerUp.transform.parent = this.transform;
    }

    IEnumerator SpawnRoutine()
    {

        yield return new WaitForSeconds(1.5f);
        while (!_stopSpawning)
        {
            ReSpawnEnemy();
            yield return new WaitForSeconds(5f);
           
        }

      
    }

    IEnumerator SpeedPowerUpRoutine()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(7, 25));
            ReSpawnSpeedPowerUp();
        }
    }

    IEnumerator TripleShotSpawnRoutine()
    {
        while (!_stopSpawning)
        {

            
            yield return new WaitForSeconds(Random.Range(7, 25));
            ReSpawnTripleShotPowerUp();
        }
    }

    IEnumerator ShieldPowerUpRoutine()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(7, 25));
            ReSpawnShieldPowerUp();
        }
    }

   

   

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
