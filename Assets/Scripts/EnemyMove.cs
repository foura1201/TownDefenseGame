using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float attackInterval = 3;

    private GameObject target;
    private bool completeStop = false;
    private bool stop = false;
    private float moveTime = 0;
    private float attackTime = 0;

    private static int moveInterval = 1;

    Rigidbody myRigidbody;
    AnimatorController myAnimator;
    EnemyLife EnemyLife;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Target");
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myAnimator = GetComponent<AnimatorController>();
        EnemyLife = GetComponent<EnemyLife>();

        myAnimator.Walk();

        direction = Vector3.Normalize(target.transform.position - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.LookRotation(direction);

        attackInterval = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (completeStop)
        {
            myRigidbody.velocity = Vector3.zero;
            attackTime += Time.deltaTime;

            if (attackTime > attackInterval)
            {
                attackTime = 0;
                if (!EnemyLife.stun)
                {
                    myAnimator.Attack();
                }
            }
        }

        else if (stop)
        {
            myRigidbody.velocity = Vector3.zero;
            moveTime += Time.deltaTime;

            if (moveTime > moveInterval)
            {
                stop = false;
                moveTime = 0;
                direction = Vector3.Normalize(target.transform.position - gameObject.transform.position);
                gameObject.transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            gameObject.transform.position += 0.05f * Time.deltaTime * direction ;
            myAnimator.Walk();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !stop && !completeStop)
        {
            stop = true;
            
            myAnimator.Idle();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !completeStop)
        {
            //Debug.Log("arrival attack");
            completeStop = true;
            if(!EnemyLife.stun) myAnimator.Attack();
            direction = Vector3.Normalize(target.transform.position - gameObject.transform.position);
            gameObject.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void SetAttackLevel(float attackLevel)
    {
        attackInterval = attackLevel;
    }
}
