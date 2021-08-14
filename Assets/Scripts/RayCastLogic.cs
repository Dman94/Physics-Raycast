using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum PLayerState
{
    Idol,
    Attack,
    Moving,
    AttackMoving
}

public class RayCastLogic : MonoBehaviour
{
    SwordLogic swordLogic;
    NavMeshAgent agent;
    [SerializeField] GameObject ClickOBj;
    float CLickObjSize;
    float MeleeRange = 1.5f;
  

    PLayerState PlayerState;
    [SerializeField] GameObject EnemyTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        swordLogic = GetComponentInChildren<SwordLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementInputCheck();
        UpdateClickObjSize();
      
        ChaseEnemyLogic();
        CheckAttackRange();

    }

     

    void MovementInputCheck()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastCameraToMouse();
        }
    }
   

    private void UpdateClickObjSize()
    {
        if (CLickObjSize > 0)
        {
            CLickObjSize -= Time.deltaTime;
            ClickOBj.transform.localScale = CLickObjSize * Vector3.one;
        }
    }

    void RaycastCameraToMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log($"We hit an object: {hit.collider.gameObject.name}");
            Debug.Log($"We hit at position: {hit.point}");

            DisplayClickOBJ(hit.point);
            agent.SetDestination(hit.point);
            agent.isStopped = false;

           

            if(hit.collider.gameObject.tag == "Enemy")
            {
                EnemyTarget = hit.collider.gameObject;
                PlayerState = PLayerState.AttackMoving;
           
            }
            else
            {
                EnemyTarget = null;
                PlayerState = PLayerState.Moving;
           
            }
        }
    }

    void DisplayClickOBJ(Vector3 pos)
    {
        if (ClickOBj)
        {
          
            CLickObjSize = 1;
            ClickOBj.transform.localScale = Vector3.one;
            ClickOBj.transform.position = pos;

        }
        

    }




    //Everything Below is related to the raycast influence on gameplay mechanics




    void CheckAttackRange()
    {
        Debug.DrawRay(agent.transform.position, agent.transform.forward * MeleeRange, Color.red);

        if (!EnemyTarget || PlayerState != PLayerState.AttackMoving) { return; } 
      
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hit, MeleeRange))
        {


            if(hit.collider.gameObject.tag == "Enemy")
            {
                if (swordLogic)
                {
                  swordLogic.SetAttacking(true);
                }

                agent.isStopped = true;
            }
        }
    }

    public EnemyLogic GetEnemyTarget()
    {
        if(EnemyTarget)
        {
            EnemyLogic enemylogic = EnemyTarget.GetComponent<EnemyLogic>();

            if (enemylogic)
            {
                return enemylogic;
            }
        }

        return null;
    }
    void ChaseEnemyLogic()
    {
        if (EnemyTarget && PlayerState == PLayerState.AttackMoving)
        {
            agent.SetDestination(EnemyTarget.transform.position);
            agent.isStopped = false;
        }
    }
}
