using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    float counter = 0;
    bool isIdle, State1;
    void Start()
    {
        anim = GetComponent<Animator>();
        isIdle = true;
        //StartCoroutine(Waiting());       
        
        
    }

    /*IEnumerator Waiting() {

        yield return new WaitForSeconds(seconds);
        Entrar();
    }*/

    public void Entrar() {
        anim.SetBool("State1", true);
        StartCoroutine(Salir());
        //Salir();
    }

    IEnumerator Salir()
    {
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("State1",false);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= 20)
        {
            Entrar();
            counter = 0;
            //Salir();
            
        }
        
    }
}
