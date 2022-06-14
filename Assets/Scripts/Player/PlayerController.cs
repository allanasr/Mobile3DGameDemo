using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed;

    public float speed;
    public string tagToCheck = "Enemy";
    public bool invincible = false;

    private Vector3 pos;
    private bool canRun;
    private float currentSpeed;
    private Vector3 startPos;

    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject coinCollector;
    public TMP_Text powerUpTxt;

    private void Start()
    {
        startPos = transform.position;
        ResetSpeed();
    }

    private void Update()
    {

        if (!canRun) return;
        pos = target.position;
        pos.y = transform.position.y;
        pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, pos, lerpSpeed * Time.deltaTime);

        transform.Translate(currentSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheck)
        {
            Debug.Log("inimigo");
            if (!invincible) EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "EndLine")
        {
            if (!invincible) EndGame();
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

    #region PowerUps

    public void SetPowerUpText(string s)
    {
        powerUpTxt.text = s;
    }
    public void PowerUpSpeed(float f)
    {
        currentSpeed = f;
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }

    public void SetInvincible(bool b)
    {
        invincible = b;
    }

    public void ChangeHeight(float amount, float duration)
    {
       /* var p = transform.position;
        p.y = startPos.y + amount;
        transform.position = p;
        Invoke(nameof(ResetHeight), duration);*/

        transform.DOMoveY(startPos.y + amount, duration).OnComplete(ResetHeight);
    }
    public void ResetHeight()
    {
        var p = transform.position;
        p.y = startPos.y;

        transform.DOMoveY(p.y, 1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
