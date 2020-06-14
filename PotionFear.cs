using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionFear : MonoBehaviour
{
    // Start is called before the first frame update
    //
    [SerializeField] int fearCuracion;
    int life;
    Stadistics st;
    void Start()
    {
        st = FindObjectOfType<Stadistics>();
        life = 75;
    }

    // Update is called once per frame
    public void HoverBottle()
    {
        //botonPick.SetActive(true);
    }

    public void PickBottle()
    {
        //botonPick.SetActive(false);
        st.MinusFear(fearCuracion);
        st.addLife(life);
        Destroy(this.gameObject);
    }

    public void PickButonOff()
    {
        //botonPick.SetActive(false);
    }
}
