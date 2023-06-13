using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
        public float startTimeBtwAttack;
    public Joystick joystick;
    public Transform attackPos;
    public LayerMask Slime;
    public float attackRange;
    public int damage;
    public Animator anim;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.controlType == Player.ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    private void Update()
    {

        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButton(0) && player.controlType == Player.ControlType.PC)
            {
                anim.SetTrigger("Attack");

            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else if (player.controlType == Player.ControlType.Android)
        {
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                anim.SetTrigger("Attack");
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        
       
    }
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, Slime);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Slime>().TakeDamage(damage);

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (attackPos.position, attackRange);
    }
}
