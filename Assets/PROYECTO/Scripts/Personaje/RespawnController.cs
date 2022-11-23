using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class RespawnController : MonoBehaviour
{
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Transform player1;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Transform player2;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Transform player3;
    [BoxGroup("Dependencias")]
    [ReadOnly] [SerializeField] private Transform player4;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player 1").GetComponent<Transform>();
        player2 = GameObject.Find("Player 2").GetComponent<Transform>();
        player3 = GameObject.Find("Player 3").GetComponent<Transform>();
        player4 = GameObject.Find("Player 4").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
