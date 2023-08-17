using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
  
    [SerializeField]
    private float _speed = 3f;

    // ID for Powerups
    // 0 = Triple Shot
    // 1 = Speed
    // 2 = Shield
    [SerializeField]
    private int powerUpID;
    [SerializeField]
    private AudioClip _audioSource;
   
  
    void Update()
    {
  
        CalculateMovement();
   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {

            AudioSource.PlayClipAtPoint(_audioSource, transform.position);

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
                        player.setIsShieldActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;

                }

            }

            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 3f);

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
