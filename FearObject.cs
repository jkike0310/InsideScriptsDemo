using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int fearObject;
    [SerializeField] GameObject boton;
    BoxCollider box;
    private Stadistics st;
    

    void Start()
    {
        st = FindObjectOfType<Stadistics>();
        box = GetComponent<BoxCollider>();
        boton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            box.enabled = false;
            if (st.contador < 1)
            {
                boton.SetActive(true);
                StartCoroutine(desactivarAvertencia());
            }
            st.PlusFear(fearObject);
            
            st.contador += 1;
        }
    }

    IEnumerator desactivarAvertencia()
    {
        yield return new WaitForSeconds(10.0f);
        boton.SetActive(false);
    }
    

}
