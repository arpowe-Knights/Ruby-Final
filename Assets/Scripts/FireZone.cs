using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    private RubyController ruby;


    public List<int> burnTickTimers = new List<int>();

    private const int DAMAGE_PER_TICK = 1;
    private const int MAX_BURNS = 3;
    private const int BURN_TIME = 1;
    private int burnsLeft = 0;
    private float cBurnTime = 0;

    void Start()
    {
        ruby = GetComponent<RubyController>();
    }

    private void Update()
    {
        if (burnsLeft <= 0)
        {
            cBurnTime = 0;
            burnsLeft = 0;
            
            if (ruby != null) ruby.smokeParticles.Stop();
        }

        else if (burnsLeft > 0)
        {
            cBurnTime -= Time.deltaTime;

            if (cBurnTime <= 0)
            {
                BurnOnce();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        RubyController rubyController = other.GetComponent<RubyController>();

        if (rubyController == null) return;

        ruby = rubyController;

        burnsLeft = MAX_BURNS;

        BurnOnce();
        ruby.smokeParticles.Play();
    }

    private void BurnOnce()
    {
        ruby.ChangeHealth(-1, becomeInvincible: false);
        burnsLeft--;
        cBurnTime = BURN_TIME;
    }

    // public void ApplyBurn(int ticks)
    // {
    //     if (burnTickTimers.Count <= 0)
    //     {
    //         burnTickTimers.Add(ticks);
    //         StartCoroutine(Burn());
    //     }
    // }

    // IEnumerator Burn()
    // {
    //     while (burnTickTimers.Count > 0)
    //     {
    //         // for(int i = 0; i < burnTickTimers.Count; i++)
    //         //{
    //         //      burnTickTimers[i]--;
    //         // }
    //         ruby.ChangeHealth(-1);
    //         // burnTickTimers.RemoveAll(i => i == 0);
    //         yield return new WaitForSeconds(0.75f);
    //     }
    // }
}


