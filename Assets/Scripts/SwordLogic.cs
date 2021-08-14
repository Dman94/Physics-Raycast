using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    Animator animator;
    RayCastLogic raycastlogic;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        raycastlogic = GetComponentInParent<RayCastLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        TriggerWeaponHit();
    }

     void TriggerWeaponHit()
    {
        
        if (animator)
        {
            SetAttacking(Input.GetButtonDown("Fire1"));
        }
       
    }

    public void WeaponHit()
    {
       
        if (raycastlogic)
        {
            
            EnemyLogic enemyLogic = raycastlogic.GetEnemyTarget();

            if (enemyLogic)
            {
                Debug.Log("FIght Me!");
                enemyLogic.TakeDamage(10);
            }
        }
        

    }

    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }
}
