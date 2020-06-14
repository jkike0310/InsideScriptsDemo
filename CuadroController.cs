using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadroController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cuadro;
    Animator anim;
    bool isMoved;
    void Start()
    {
        anim = cuadro.GetComponent<Animator>();
        isMoved = false;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) {

            if (isMoved==false) { 
                anim.SetBool("MoveCuadro", true);
                this.gameObject.GetComponent<AudioSource>().Play();
                isMoved = true;
            }
            

        }
    }
}
