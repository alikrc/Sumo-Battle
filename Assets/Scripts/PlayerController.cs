using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    private Rigidbody playerRb;
    private GameObject focalPoint;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = this.transform.position - new Vector3(0, 0.5f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
            powerupIndicator.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        var enemyGameObject = other.gameObject;
        if (enemyGameObject.CompareTag("Enemy") && hasPowerup)
        {
            var enemyRb = enemyGameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = enemyGameObject.transform.position - this.transform.position;

            enemyRb.AddForce(awayFromPlayer * 15f, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}
