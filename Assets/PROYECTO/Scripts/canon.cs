using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon : MonoBehaviour
{

    public Transform Mira_canon;
    public GameObject Bala_recta_prefab;
    public float pausa;
    // Start is called before the first frame update
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
        GameObject disparo = Instantiate(Bala_recta_prefab, Mira_canon.position, Mira_canon.rotation);

        StartCoroutine(Disparar());
    }
}
