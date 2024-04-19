using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitor : Singleton<Transitor>
{
    [SerializeField] private Animator anim;
    [SerializeField] private float duration;
    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void StartTransition()
    {
        StartCoroutine(TransitionCo());
    }
    private IEnumerator TransitionCo()
    {
        anim.SetTrigger("transIn");
        yield return new WaitForSeconds(duration);
        anim.SetTrigger("transOut");
    }
}
