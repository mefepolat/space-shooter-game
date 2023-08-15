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

    private BoxCollider2D _boxCollider2D;
    private AudioSource _audioSource;

    private Animator _animator;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("PLAYER IS NULL");
        }
        _animator = gameObject.GetComponent<Animator>();
        _boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
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
        
        if (other.tag == "Laser")
        {
           
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
            _boxCollider2D.enabled = false;
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
                _boxCollider2D.enabled = false;
                Destroy(this.gameObject, 2.8f);
               
                
            }

            
            
            
        }
        _audioSource.Play();
        
    }
}
