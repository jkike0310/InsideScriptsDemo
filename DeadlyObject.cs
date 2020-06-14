using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObject : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage = 100;
    Stadistics st;
    private void Start()
    {
        st = FindObjectOfType<Stadistics>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            st.restLife(damage);
        }
    }
}
