using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRb;
    private float speed = 5;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var lookDirection = (player.transform.position - enemyRb.transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed, ForceMode.Acceleration);

        if (this.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }
}
