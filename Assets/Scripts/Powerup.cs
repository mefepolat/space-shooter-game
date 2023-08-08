using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 3f;

    // ID for Powerups
    // 0 = Triple Shot
    // 1 = Speed
    // 2 = Shield
    [SerializeField]
    private int powerUpID;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3
        CalculateMovement();
        // when we leave the screen, destroy this object
    }

    // On Trigger Collision
    // Only be collectable by the player (use tags)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            
            Destroy(this.gameObject);

            Player player = collision.transform.GetComponent<Player>();
            
            if (player != null)
            {
                switch (powerUpID)
                {
                    case 0:
                        player.setIsTripleShotActive();
                        break;
                    case 1:
                        player.setIsSpeedActive();
                        break;
                    case 2:
                        Debug.Log("Shield power is activated");
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;

                }

            }




        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.49)
        {
            Destroy(this.gameObject);
        }
    }
}
