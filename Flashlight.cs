using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject hand;
    private Enemy enemy;
    private int damageFlash=25;
    public float range = 150f;
    public float range2 = 75f;
    public float battery = 100f;
    private bool flashPressed = false;
    private bool batterynull = false;

    

    float sliderValue;
    public Camera flashlight;
    public GameObject sliderObject;
    Slider flashlightBar;
    Animator anim;
    Animator animHand;

    private void Start()
    {
        
        anim = GetComponent<Animator>();
        animHand = hand.GetComponent<Animator>();
        animHand.SetBool("isIdleHand", true);
        flashlightBar = sliderObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    

    private void Update()
    {

        CheckRayCast();
        enemy = FindObjectOfType<Enemy>();

        if (batterynull)
        {
            StartCoroutine(Shaking());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !batterynull)
        {
            sliderObject.SetActive(true);
            flashPressed = true;
            anim.SetBool("FlashDamage", true);
            FlashShoot();
            StartCoroutine(restBattery());
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            sliderObject.SetActive(false);
            flashPressed = false;
            anim.SetBool("FlashDamage", false);
        }
        sliderValue = (battery / 100);
        flashlightBar.value = sliderValue;
        
    }
    private void CheckRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(flashlight.transform.position, flashlight.transform.forward, out hit, range2))
        {
            //Debug.Log(hit.transform.name);
            PotionFear potion = hit.transform.GetComponent<PotionFear>();
            Key KeyDoor = hit.transform.GetComponent<Key>();
            JeringaHealth jeringa = hit.transform.GetComponent<JeringaHealth>();
            if (potion != null)
            {
                //Debug.Log("Enemigo recibe daño");
                potion.HoverBottle();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    potion.PickBottle();
                }
            }

            if (KeyDoor != null)
            {
                //Debug.Log("Enemigo recibe daño");
                KeyDoor.HoverKey();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    KeyDoor.PickKey();
                }
            }
            if (jeringa != null)
            {
                //Debug.Log("Enemigo recibe daño");
                jeringa.HoverJeringa();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    jeringa.PickJeringa();
                }
            }

        }
    }
    private void FlashShoot()
    {
        if (battery<=0)
        {
            batterynull = true;
        }
        
        
        RaycastHit hit;
        if(Physics.Raycast(flashlight.transform.position, flashlight.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            EnemyBoy enemy2 = hit.transform.GetComponent<EnemyBoy>();
            if (enemy != null)
            {
                //Debug.Log("Enemigo recibe daño");
                enemy.takeDamage(damageFlash);
            }
            if(enemy2 != null)
            {
                enemy2.takeDamage(damageFlash);
            }
        }
    }

    IEnumerator Shaking()
    {
        anim.SetBool("BatteryNull", true);
        animHand.SetBool("isShakingHand", true);
        yield return new WaitForSeconds(3f);        
        batterynull = false;
        battery = 100f;
        anim.SetBool("BatteryNull", false);
        animHand.SetBool("isShakingHand", false);
    }

    IEnumerator restBattery()
    {
        battery-=5;
        yield return new WaitForSeconds(1);
        if (flashPressed && battery >0)
        {
            Reiniciar();
        }
        else if (battery <= 0)
        {
            StartCoroutine(Shaking());
        }
    }
        

    void Reiniciar(){
        StartCoroutine(restBattery());
    }
    
    
}
