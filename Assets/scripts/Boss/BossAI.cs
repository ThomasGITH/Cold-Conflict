using UnityEngine;

public class BossAI : MonoBehaviour {

    public GameObject Laser;
    public GameObject GFL;
    public GameObject GFLWarn;

    public float Health = 500f;

    [HideInInspector]
    public int WhatAttack = 0;
    public int AttackChance = 50;

    [HideInInspector]
    public bool IsAttacking = false;
    [HideInInspector]
    public float GFLtimer = 1f;
    [HideInInspector]
    public float GFLWarntimer = 1f;

    public float GFLWarnTimerStart = 10f;
    public float GFLTimerStart = 1.5f;
    [HideInInspector]
    public float timer = 1f;
	
	void Update () {
        AllTimers();
        WhatToDo();
    }

    public void AllTimers()
    {
        timer = timer - 1 * Time.deltaTime;
        GFLWarntimer = GFLWarntimer - 1 * Time.deltaTime;
        GFLtimer = GFLtimer - 1 * Time.deltaTime;
    }

    public void WhatToDo()
    {
      WhatAttack = Random.Range(0, AttackChance);
        switch (WhatAttack)
        {
            case 28:
                if (!IsAttacking)
                {
                    IsAttacking = true;
                    LaserBurst();
                }
              break;

            case 3:

                if (!IsAttacking)
                {
                    GFLtimer = GFLTimerStart;
                    GFLWarntimer = GFLWarnTimerStart;
                }
                IsAttacking = true;
                ShootGFL();
             break;
        }
    }

    public void LaserBurst()
    {
        Instantiate(Laser, transform.position + (transform.forward * 0.4f), transform.rotation);              
    }

    public void ShootLaser()
    {
        int j = 0;

        for (int i = 0; i < 10; i++)
        {
            LaserBurst();
            j++;
        }

        if(j < 10)
        {
            IsAttacking = false;
        }
        
    }

    public void ShootGFL()
    {
        
         GFLWarn.SetActive(true);
            
        if (GFLWarntimer <= 0)
        {
            GFLWarn.SetActive(false);
            GFL.SetActive(true);
        }
        
        if (GFLtimer <= 0)
        {
            GFL.SetActive(false);
            IsAttacking = false;
        }        
    }
}
