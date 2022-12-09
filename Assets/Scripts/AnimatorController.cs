using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public int AttackLevel;

    GameObject varManager;
    WallHP WallHP;
    Animator Animator;
    Phase phase;
    private float attackTime = 0;
    private bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        varManager = GameObject.Find("VariableManagers");
        WallHP = varManager.transform.GetChild(1).GetComponent<WallHP>();
        phase = varManager.transform.GetChild(0).GetComponent<Phase>();
        int phasevariable = phase.phaseVar;
        AttackLevel *= phasevariable;
    }

    // Update is called once per frame
    void Update()
    {
        if(attack)
        {
            attackTime += Time.deltaTime;
            if(attackTime > 1)
            {

                Idle();
                attack = false;
                attackTime = 0;
            }
        }
    }
    public void Walk()
    {
        Animator.SetBool("walk", true);
    }
    public void Attack()
    {
        Animator.SetBool("idle", false);
        Animator.SetBool("attack", true);
        attack = true;

        WallHP.WallBreak(AttackLevel);
    }

    public void GetHit()
    {
        //Debug.Log("get hit");
        Animator.SetBool("getHit", true);
    }

    public void Idle()
    {
        //Debug.Log("idle");
        Animator.SetBool("getHit", false);
        Animator.SetBool("walk", false);
        Animator.SetBool("attack", false);
        Animator.SetBool("idle", true);
    }

    public void Die()
    {
        //Debug.Log("die");
        Animator.SetBool("getHit", false);
        Animator.SetBool("walk", false);
        Animator.SetBool("attack", false);
        Animator.SetBool("idle", false);
        Animator.SetBool("die", true);
    }
}
