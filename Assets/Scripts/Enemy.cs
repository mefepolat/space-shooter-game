using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 4f;
    
    private float minX = -6f;
    private float maxX = 12f;

    private Player _player;

    [SerializeField]
    private GameObject _enemyFab;

    private Animator _animator;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("PLAYER IS NULL");
        }
        _animator = gameObject.GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.Log("animator is null");
        }
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
        BoxCollider2D boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
        if (other.tag == "Laser")
        {
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
            boxCollider.enabled = false;
            _player.increaseScore(10);
            Destroy(other.gameObject);
            
        }  
        
        if(other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                other.transform.GetComponent<Player>().Damage();
                _animator.SetTrigger("OnEnemyDeath");
                boxCollider.enabled = false;
                Destroy(this.gameObject, 2.8f);
               
                
            }

            
            
            
        }

        
    }
}
