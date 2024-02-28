using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mauro : MonoBehaviour
{
    private Unit_Controller mauro;

    public float secondsToSmoke = 4f;

    private float elapsedTime = 0f;

    void Start()
    {
        mauro = GetComponent<Unit_Controller>();   
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > secondsToSmoke)
        {
            PlayAnimation("Smoke");
            elapsedTime = 0f;
        }
    }

    private void PlayAnimation(string anim_name)
    {
        if (anim_name.Equals("Smoke"))
        {
            mauro.animator.SetTrigger("Smoke");
        }
    }
}
