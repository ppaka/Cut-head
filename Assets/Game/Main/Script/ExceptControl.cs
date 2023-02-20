using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExceptControl : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spRenderer;
    public ProgressBar progressBar;

    private void Start()
    {
        CallRandom();
    }

    public void CallRandom()
    {
        StartCoroutine("RandomStart");
    }

    private IEnumerator RandomStart()
    {
        yield return new WaitForSeconds(1.2f);
        var i = Random.Range(1, 101);
        if (i >= 84)
        {
            animator.SetBool("dontwork", true);
        }
        else
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine("RandomStart");
        }
    }
    
    public void NotTouch()
    {
        PlayerPrefs.SetInt("gameProgress", PlayerPrefs.GetInt("gameProgress", 0) - 2);
        progressBar.UpdateImage();
        spRenderer.DOKill(true);
        spRenderer.color = new Color(0.8f, 0.2f, 0.2f, 1);
        spRenderer.DOColor(new Color(1, 1, 1, 1), 1f);
        animator.SetBool("dontwork", false);
        CallRandom();
    }

    public void Touch()
    {
        if (!animator.GetBool("dontwork")) return;
        PlayerPrefs.SetInt("gameProgress", PlayerPrefs.GetInt("gameProgress", 0) + 1);
        progressBar.UpdateImage();
        spRenderer.DOKill(true);
        spRenderer.color = new Color(0.3f, 0.9f, 0.5f, 1);
        spRenderer.DOColor(new Color(1, 1, 1, 1), 1f);
        animator.SetBool("dontwork", false);
        CallRandom();
    }
}