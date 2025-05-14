using UnityEngine;
using System.Collection.Generic;

public class EnemyScript : MonoBehaviour
{
    public int health = 2;
    Animator anim;
    bool Dead = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(health<= 0)
        Death();
    }
    private void Death()
    {
        anim.SetBool("Dead", true);
    }
}