﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // data type (int, float, boolean, string)
    // optional value assigned
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _nextFire = -1f;
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private GameObject _tripleShotPrefab;

 

    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private bool _isSpeedActive = false;

    [SerializeField]
    private bool _isShieldActive = false;
    public int getLives()
    {
        return this._lives;
    }

   

    // Start is called before the first frame update
    void Start()
    {
        // take current position = new position (0, 0 , 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        // when we hit the space key spawn the game Object (laser)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }

        StartCoroutine(TripleShotRoutine());

       
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

       
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        

        
             transform.Translate(direction * _speed  * Time.deltaTime);


        



        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
       

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            
            Vector3 position = new Vector3(transform.position.x - -0.5127084f, transform.position.y + 0.6547515f, transform.position.z - -1.455253f);
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        } 
        else
        {
            
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 1.011f, transform.position.z);
            Instantiate(_laserPrefab, position, Quaternion.identity);
        }
        

        
    }

    public void Damage()
    {

        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        _lives--;
        if(_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void setIsTripleShotActive()
    {
        _isTripleShotActive = true;
        
    }

    public void setIsShieldActive()
    {
        _isShieldActive = true;
        transform.GetChild(0).gameObject.SetActive(true);
        
    }

    public void setIsSpeedActive()
    {
        
        _isSpeedActive = true;
        _speed = _speed * _speedMultiplier;
        StartCoroutine(SpeedPowerUpRoutine());
    }

  
    IEnumerator SpeedPowerUpRoutine()
    {
        while (_isSpeedActive)
        {
            
            yield return new WaitForSeconds(5);
            _isSpeedActive = false;
            _speed /= _speedMultiplier;
        }
    }

    IEnumerator TripleShotRoutine()
    {
        while (_isTripleShotActive)
        {
            yield return new WaitForSeconds(5);
            _isTripleShotActive = false;
            
        }
    }


}
