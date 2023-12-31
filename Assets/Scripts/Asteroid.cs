﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 100.0f;


    [SerializeField]
    private GameObject _asteroid;

    [SerializeField]
    private GameObject _explosion;
    

    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
       
        
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Laser")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
            Destroy(collision.gameObject);
            
        }
        
    }
}
