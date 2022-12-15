using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class canon_proyectil : MonoBehaviour
{
    public GameObject bala_proyectil_prefab;
    public Transform Mira_canon;
    public int velocidad;
    public float pausa;
    public SpriteRenderer spriteRender;
    
    private float velocidad_x;
    private float velocidad_y;
    void Start()
    {
        StartCoroutine(Disparar());
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator Disparar()
    {
        yield return new WaitForSeconds(pausa);

        if (!spriteRender.flipX)
        {
            Vector3 position = new Vector3(0.87f, 0.87f, 0);

            Mira_canon.transform.localPosition = position;
            Mira_canon.transform.Rotate(0, 0, 0, Space.Self);

            velocidad_x = velocidad * -1;
            velocidad_y = velocidad - 9.8f * Time.deltaTime;
            GameObject disparo = Instantiate(bala_proyectil_prefab, Mira_canon.position, Mira_canon.rotation);
            disparo.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidad_x, velocidad_y), ForceMode2D.Impulse);
        }
        else
        {
            velocidad_x = velocidad;
            velocidad_y = velocidad - 9.8f * Time.deltaTime;
            
            Vector3 position = new Vector3(-0.87f, 0.87f, 0);
                        
            Mira_canon.transform.localPosition = position;
            Mira_canon.transform.Rotate(0, 0, 0, Space.Self);
            GameObject disparo = Instantiate(bala_proyectil_prefab, Mira_canon.position, Mira_canon.rotation);
            
            disparo.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidad_x, velocidad_y), ForceMode2D.Impulse);
        }

        StartCoroutine(Disparar());
    }
}
