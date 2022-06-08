using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed;

    public float speed;
    public string tagToCheck = "Enemy";

    private Vector3 pos;
    private bool canRun;

    public GameObject startScreen;
    public GameObject endScreen;

    private void Update()
    {

        if (!canRun) return;
        pos = target.position;
        pos.y = transform.position.y;
        pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, pos, lerpSpeed * Time.deltaTime);

        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == tagToCheck)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "EndLine")
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        canRun = false;
        endScreen.SetActive(true);
        LoadScene.Instance.Load(0);
    }

    public void StartGame()
    {
        canRun = true;
        startScreen.SetActive(false);
    }  
    public void Restart()
    {
        canRun = true;
        endScreen.SetActive(false);
    }
}
