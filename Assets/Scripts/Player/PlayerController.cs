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
    private float baseAnimationSpeed = 10f;

    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject coinCollector;
    public TMP_Text powerUpTxt;

    [Header("Limits")]
    public float limit = 4;

    [Header("Animation")]
    public AnimationManager animationManager;

    [SerializeField] BounceHelper bounceHelper;

    private void Start()
    {
        startPos = transform.position;
        ResetSpeed();
        StartAnimation();
    }

    private void StartAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 1f).SetEase(Ease.OutBounce);
    }
    public void Bounce()
    {
        if (bounceHelper != null)
            bounceHelper.Bounce();
    }
    private void Update()
    {

        if (!canRun) return;
        pos = target.position;
        pos.y = transform.position.y;
        pos.z = transform.position.z;

        if (pos.x > -limit) pos.x = -limit;
        else if (pos.x < limit) pos.x = limit;

        transform.position = Vector3.Lerp(transform.position, pos, lerpSpeed * Time.deltaTime);

        transform.Translate(currentSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheck)
        {
            if (!invincible) EndGame(AnimationManager.AnimationType.DEAD);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "EndLine")
        {
            EndGame(AnimationManager.AnimationType.IDLE);
        }
    }
    private void EndGame(AnimationManager.AnimationType animationType = AnimationManager.AnimationType.IDLE)
    {
        canRun = false;
        endScreen.SetActive(true);
        animationManager.SetTrigger(animationType);
        transform.DOMoveZ(-1f, 1).SetRelative();
    }

    public void StartGame()
    {
        canRun = true;
        startScreen.SetActive(false);
        animationManager.SetTrigger(AnimationManager.AnimationType.RUN, currentSpeed / baseAnimationSpeed);
    }
    public void Restart()
    {
        canRun = true;
        LoadScene.Instance.Load(0);
    }

    #region PowerUps

    public void SetPowerUpText(string s)
    {
        powerUpTxt.text = s;
    }
    public void PowerUpSpeed(float f)
    {
        currentSpeed = f;
        animationManager.SetTrigger(AnimationManager.AnimationType.RUN, currentSpeed / baseAnimationSpeed);

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
