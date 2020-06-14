using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoy : MonoBehaviour
{
    // Start is called before the first frame update
    CapsuleCollider collider;
    //PRUEBA.---!!!!!!!!
    public float lookRadius = 25f;
    
    Transform target;
    NavMeshAgent nav;
    
    //
    Animator anim;
    [SerializeField] int life;
    int damage;
    private Stadistics st;
    
    bool isIdle;
    bool isWalk;
    bool isAttack;
    bool isDefense;
    bool isDead;
    int contador = 0;
    int contador2 = 0;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;

        //
        collider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        st = FindObjectOfType<Stadistics>();
        
        
        life = 100;
        damage = 25;        
        anim.SetBool("isIdleBoy", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            anim.SetFloat("Distance", distance);
            if (distance > 50)
            {
                //nav.speed = 0;
                isIdle = true;
                anim.SetBool("isIdleBoy", true);
                DetenerAnim();
            }
            
            else if (distance <= 50 && distance > 10)
            {
                if (!isDefense)
                {
                    isWalk = true;
                    //nav.speed = 20;
                    FaceTarget();
                    nav.SetDestination(target.position);
                    anim.SetBool("isRunBoy", true);
                    anim.SetBool("isIdleBoy", false);
                    anim.SetBool("isAttackBoy", false);
                }
               
                //anim.SetBool("isDefenseMonster", false);
                //anim.SetBool("isRetorcerMonster", false);


                //nav.Stop();

            }
            else if (distance <= 10)
            {

                //anim.SetBool("isAttackMonster", true);
                StartCoroutine(Attack());
                FaceTarget();
            }
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void DetenerAnim()
    {
        anim.SetBool("isAttackBoy", false);
        anim.SetBool("isRunBoy", false);
        //anim.SetBool("isDefenseMonster", false);
        
    }

    IEnumerator Attack()
    {
        anim.SetBool("isRunBoy", false);
        anim.SetBool("isAttackBoy", true);
        anim.SetBool("isIdleBoy", false);
        yield return new WaitForSeconds(1f);
        isAttack = true;
        //nav.speed = 0f;
        //nav.acceleration = 0; TTTTTT
        
        //anim.SetBool("isDefenseMonster", false);
        //anim.SetBool("isRetorcerMonster", false);
        //yield return new WaitForSeconds(1.5f);
        //anim.speed = 0.7f;
        nav.SetDestination(this.transform.position);
        if (contador < 1)
        {
            st.restLife(damage);
            contador++;
        }

        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        anim.SetBool("isAttackBoy", false);
        //anim.speed = 1f;
        //nav.speed = 20;
        //nav.acceleration = 10;
        contador = 0;

    }

    public void takeDamage(int damageFlash)
    {
        nav.SetDestination(this.transform.position);
        //nav.speed = 0;
        //nav.acceleration = 0;
        isDefense = true;
        life -= damageFlash;
        //Animation Here
       
        anim.SetBool("isDefenseBoy", true);
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
        anim.SetBool("isDieBoy", true);
        //nav.speed = 0;
        //nav.acceleration = 0;
        Destroy(this.gameObject, 4f);
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
        //nav.speed = 0;
        //nav.acceleration = 0;
        yield return new WaitForSeconds(3.5f);
        isDefense = false;
        anim.SetBool("isDefenseBoy", false);
        collider.enabled = true;
        //nav.speed = 20;
        //nav.acceleration = 10;
    }
}
