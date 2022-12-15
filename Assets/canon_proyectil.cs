using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon_proyectil : MonoBehaviour
{
    public GameObject bala_proyectil_prefab;
    public Transform Mira_canon;
    public int velocidad;
    public float pausa;

    private float velocidad_x;
    private float velocidad_y;
    void Start()
    {
        StartCoroutine(Disparar());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator Disparar()
    {
       
        yield return new WaitForSeconds(pausa);
        velocidad_x = velocidad;
        velocidad_y = velocidad - 9.8f * Time.deltaTime;
        GameObject disparo = Instantiate(bala_proyectil_prefab, Mira_canon.position, Mira_canon.rotation);
        disparo.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidad_x, velocidad_y), ForceMode2D.Impulse);


        StartCoroutine(Disparar());
    }
}
