using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    
    public Transform Player;
    public float speed;
    public float agroDistance;
    public int health;
    public int damage;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    private Player player;
    private Animator anim; 
    private AddRoom room; //Если че удалить
    private float timeBtwAttack;
    public float startTimeBtwAttack;


    public void TakeDamage (int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }
    void Start()
    {
       
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
        room = GetComponentInParent<AddRoom>(); //Если че удалить
        
    }

    
    void Update()
    {
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if (health < 0)
        {
            Destroy(gameObject);
           room.enemies.Remove(gameObject); //Если че удалить
        }
        float distToPlayer = Vector2.Distance(transform.position, Player.position);
       if (distToPlayer < agroDistance) 
        {
            StartHunting();

        }
       else
        {
            StopHunting();
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
            if (timeBtwAttack <= 0)
            {
                anim.SetTrigger("SlimeAttack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void OnEnemyAttack()
    {
        player.ChangeHealth (-damage);
        timeBtwAttack = startTimeBtwAttack;
    }
    void StartHunting()
    {

        if(Physics2D.OverlapCircle(transform.position, 40f))
        {
            //physic.velocity = new Vector2(-speed, 0);
            Vector3 direction = new Vector3(Player.position.x - transform.position.x, Player.position.y - transform.position.y, Player.position.z - transform.position.z);
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    
    }
    void StopHunting()
    {
        
    }
}
