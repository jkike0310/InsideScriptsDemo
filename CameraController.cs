using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController sharedInstance; // Se instancia solo una camara compartida
    public GameObject FollowTarget; // Objetivo de la camara
    private TextP text;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject Play;
    [SerializeField] GameObject Team;
    [SerializeField] GameObject Exit;
    Animator anim;
    Animator anim2;
    Animator anim3;
    Animator anim4;

    // Suavizar el movimiento y rotación de la cámara
    private float movementSmoothness = 0.4f;
    private float rotationSmoothness = 1.0f;

    bool canFollow = false; // Saber si hay un objetivo

    private void Awake()
    {
        //Se inicializa la cámara
        text = FindObjectOfType<TextP>();
        sharedInstance = this;
    }
    private void Start()
    {
        anim = menu.GetComponent<Animator>();
        anim2 = Play.GetComponent<Animator>();
        anim3 = Team.GetComponent<Animator>();
        anim4 = Exit.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            canFollow = true;
            text.HideText();
            text.image.SetActive(false);
            StartCoroutine(FadeInMenu());
        }
    }

    //Update tardío, último frame.
    private void LateUpdate()
    {
        //En caso de personaje muerto o hay cinemáticas
        if (FollowTarget == null || canFollow == false)
        {
            return;
        }
        //Interpolación lineal (Punto partida, punto llegada, velocidad)
        transform.position = Vector3.Lerp(transform.position, FollowTarget.transform.position, Time.deltaTime * movementSmoothness);
        //Interpolación esférica para rotación (Punto partida, punto llegada, velocidad)
        transform.rotation = Quaternion.Slerp(transform.rotation, FollowTarget.transform.rotation, Time.deltaTime * rotationSmoothness);
    }

    IEnumerator FadeInMenu()
    {
        yield return new WaitForSeconds(12.5f);
        anim.SetBool("MenuIn", true);
        anim2.SetBool("PlayIn", true);
        anim3.SetBool("TeamIn", true);
        anim4.SetBool("ExitIn", true);
    }
}
