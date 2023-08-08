using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 4f;
    
    private float minX = -8f;
    private float maxX = 8f;

    private Player _player;

    [SerializeField]
    private GameObject _enemyFab;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
       
    }

    
 
    void Update()
    {
       
        EnemyMovement();
        WrapEnemy();
        
    }

    float XGenerator()
    {
        float x = Random.Range(minX, maxX);
        
        return x;
    }

     void WrapEnemy()
    {

        float x = XGenerator();

        if (transform.position.y < -4.49)
        {
            transform.position = new Vector3(x, 7, 0);
            
        }

    }

   

    void EnemyMovement()
    {
        int leftLives = _player.getLives();
        if(leftLives > 0)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
           
        
        
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            
        }  
        
        if(other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                other.transform.GetComponent<Player>().Damage();
                
                Destroy(this.gameObject);
               
                
            }

            
            
            
        }
        
        
    }
}
