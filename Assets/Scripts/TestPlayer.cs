using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float shootSpeed;

    private static string state = "Running";
    private Animator[] animators;

    private bool running;
    private bool shooting;

    private void Start()
    {
        animators = transform.parent.gameObject.GetComponentsInChildren<Animator>();
        
        foreach (Animator anim in animators)
        {
            anim.logWarnings = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTrigger("ShootRequest");
        }

        foreach (Animator anim in animators)
        {
            anim.SetBool("Running", running); 
            anim.SetBool("Shooting", shooting);
            anim.SetFloat("ShootSpeed", shootSpeed);
        }
    }

    public void ShootStart()
    {
        SetTrigger("ShootStart");
        shooting = true;
    }

    public void Shoot()
    {
        Debug.Log("Arrow shot!");
        StartCoroutine(ShotDelay(2f / (shootSpeed * 10f)));
    }

    public IEnumerator ShotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetTrigger("Shot");
    }

    public void ShootEnd()
    {
        SetTrigger("ShootEnd");
        shooting = false;
    }

    private void SetTrigger(string name)
    {
        foreach (Animator anim in animators)
        {
            anim.SetTrigger(name);
        }
    }
}
