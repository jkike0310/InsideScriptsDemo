using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] GameObject animatorController;
    CapsuleCollider collider;
    //PRUEBA.---!!!!!!!!
    public float lookRadius=25f;
    AudioSource audioPer;
    Transform target;
    NavMeshAgent nav;
    //
    Animator anim;
    
    private Stadistics st;
    [SerializeField] int life;
    int damage;
   
    

    bool isIdle;
    bool isWalk;
    bool isAttack;
    bool isDefense;
    bool isDead;
    int contador =0;
    int contador2 = 0;

    void Start()
    {
        //PRUEBA
        nav = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        
        //
        collider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        st = FindObjectOfType<Stadistics>();
        life = 100;
        damage = 15;
        audioPer = GetComponent<AudioSource>();
        anim.SetBool("isIdleMonster", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            anim.SetFloat("Distance", distance);
            if (distance > 70)
            {
               
                isIdle = true;
                anim.SetBool("isIdleMonster", true);
                DetenerAnim();
            }else if(distance<=70 && distance>50){
                PrepareMonster();
                
            }
            else if (distance <= 50 && distance > 10)
            {
                if (!isDefense)
                {
                    
                    isWalk = true;
                    FaceTarget();
                    nav.SetDestination(target.position);
                    anim.SetBool("isWalkingMonster", true);
                    anim.SetBool("isIdleMonster", false);
                    anim.SetBool("isAttackMonster", false);
                    //anim.SetBool("isDefenseMonster", false);
                    anim.SetBool("isRetorcerMonster", false);
                }
                


                //nav.Stop();

            }
            else if (distance <= 10)
            {

                //anim.SetBool("isAttackMonster", true);
                StartCoroutine(Attack());
                FaceTarget();
            }

            if(distance <= 50)
            {
                st.persecution = true;
            }
        }
        
        /*else if(distance <= lookRadius && distance >= lookRadius - 10)
        {
            anim.SetBool("isRetorcerMonster",true);
            anim.SetBool("isIdleMonster", false);
        }
        else if(distance <= lookRadius-10 && distance >= lookRadius-35)
        {
            nav.SetDestination(target.position);
            anim.SetBool("isRetorcerMonster", false);
            anim.SetBool("isIdleMonster", false);
            anim.SetBool("isWalkingMonster", true);
            
            
        }
        else if(distance<lookRadius-30)
        {
            anim.SetBool("isWalkingMonster", false);
            anim.SetBool("isAttackMonster", true);
            nav.SetDestination(target.position);
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            audioPer.Play();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void DetenerAnim()
    {
        anim.SetBool("isAttackMonster", false);
        anim.SetBool("isWalkingMonster", false);
        //anim.SetBool("isDefenseMonster", false);
        anim.SetBool("isRetorcerMonster", false);
    }

    void PrepareMonster()
    {
        if (contador2 < 1)
        {
            anim.SetBool("isRetorcerMonster", true);
            contador2++;
        }
        
    }

    IEnumerator Attack()
    {
        anim.SetBool("isWalkingMonster", false);
        anim.SetBool("isAttackMonster", true);       
        anim.SetBool("isIdleMonster", false);        
        //anim.SetBool("isDefenseMonster", false);
        anim.SetBool("isRetorcerMonster", false);
        yield return new WaitForSeconds(1f);
        isAttack = true;
        //yield return new WaitForSeconds(1.5f);
        nav.SetDestination(this.transform.position);
        //anim.speed = 0.4f;
        
        if (contador < 1)
        {
            st.restLife(damage);
            contador++;
        }
        
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttackMonster", false);
        anim.speed = 1f;
        
        contador = 0;
        
    }

    public void takeDamage(int damageFlash)
    {
        nav.SetDestination(this.transform.position);
        isDefense = true;
        life -= damageFlash;
        
        //Animation Here
        anim.SetBool("isDefenseMonster", true);
        collider.enabled = false;
        StartCoroutine(Invulnerabilidad());
        
        
        
        //Stop movement for 5 seconds
        if (life <= 0)
        {
            die();
        }
        
        
    }

    public void die()
    {
        //Animation here
        nav.SetDestination(this.transform.position);
        isDead = true;
        anim.SetBool("isDeadMonster", true);
        nav.speed = 0;
        nav.acceleration = 0;
        Destroy(this.gameObject,4f);
    }


    /*void Attack()
    {
        //Animation Here
        anim.SetBool("isAttackMonster",true);
        st.restLife(damage);
        //Stop movement for 3 seconds
    }*/

    IEnumerator Invulnerabilidad()
    {
        
        yield return new WaitForSeconds(2.7f);
        isDefense = false;
        anim.SetBool("isDefenseMonster", false);
        collider.enabled = true;
        
    }

    
}
