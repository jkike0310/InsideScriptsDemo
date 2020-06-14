using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject key3d;
    Stadistics st;

    void Start()
    {
        st = FindObjectOfType<Stadistics>();
    }
    public void HoverKey()
    {
        //botonPick.SetActive(true);
    }
    public void PickKey()
    {
        //botonPick.SetActive(false);
        st.haveKey = true;
        StartCoroutine(key3D());
        Destroy(this.gameObject);
    }

    IEnumerator key3D()
    {
        key3d.SetActive(true);
        yield return new WaitForSeconds(6f);
        key3d.SetActive(false);
    }
}
