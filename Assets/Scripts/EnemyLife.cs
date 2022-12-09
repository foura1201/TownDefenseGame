using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int Life;
    public bool stun = false;

    //private float time = 0;
    private float dieTime = 0;
    private float stunTime = 0;
    private static float dieInterval = 2;
    private static float stunInterval = 3;

    AnimatorController myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<AnimatorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Life <= 0)
        {
            dieTime += Time.deltaTime;
            if(dieTime > dieInterval) Destroy(gameObject);
        }

        if (stun)
        {
            myAnimator.Idle();
            stunTime += Time.deltaTime;
            if (stunTime > stunInterval)
            {
                stun = false;
                stunTime = 0;
            }
        }

        /*time += Time.deltaTime;
        if (time > 7)
        {
            GetHit();
            stun = true;
            time = 0;
        }*/
    }

    public void GetHit(int strength)
    {
        Life -= strength;
        if(Life > 0) myAnimator.GetHit();
        else myAnimator.Die();
    }
}
