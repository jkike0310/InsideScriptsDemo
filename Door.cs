using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject a;    
    [SerializeField] GameObject a2;
    BoxCollider box;
    Animator anim;
    Animator anim2;
    [SerializeField] private GameObject boton;
    
    //[SerializeField] private TextMeshProUGUI text2;
    void Start()
    {
        anim = a.GetComponent<Animator>();
        anim2 = a2.GetComponent<Animator>();
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        boton.SetActive(true);
        //text2.enabled = true;

    }

    private void OnTriggerStay(Collider other)
    {
        boton.SetActive(true);
        if (other.tag.Equals("Player")) {
            boton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X)) {
                anim.SetBool("IsOpen",true);
                anim2.SetBool("IsOpen", true);
                boton.SetActive(false);
                box.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        boton.SetActive(false);
        //text2.enabled = false;
        if (other.tag.Equals("Player")) {
            anim.SetBool("IsOpen", false);
            anim2.SetBool("IsOpen", false);
        }
    }
    
}
