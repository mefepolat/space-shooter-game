﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyFab;
    private float minX = -8f;
    private float maxX = 8f;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _tripleShotFab;

    [SerializeField]
    private GameObject _tripleShotContainer;

    [SerializeField]
    private GameObject _speedFab;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(TripleShotSpawnRoutine());
        StartCoroutine(SpeedPowerUpRoutine());
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

    IEnumerator SpawnRoutine()
    {
        
        
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

   

    public void setStopSpawning(bool _stopSpawning)
    {
        this._stopSpawning = _stopSpawning;
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}