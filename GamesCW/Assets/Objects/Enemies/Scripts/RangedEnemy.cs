using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedEnemy : Enemy
{

    public float speed;
    public Projectile projectile;
    public float shootingRange;
    public float projectileSpeed;

    protected override void Awake()
    {
        //garabge
        projectileSpeed = Random.Range(projectileSpeed, projectileSpeed * 1.5f);
        speed = Random.Range(speed, speed * 1.5f);
        shootingRange = Random.Range(shootingRange, shootingRange * 2f);
        base.Awake();
        StartCoroutine(AI());

    }

    IEnumerator AI()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if(Vector3.Distance(transform.position, target.position) < shootingRange)
            {
                yield return StartCoroutine(Shoot());
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
                cc.Move(transform.forward * speed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        float timer = 0;
        Projectile projectileInstance = Instantiate(projectile, transform.position + (transform.forward/2), Quaternion.LookRotation(transform.forward.normalized, Vector3.up));
        projectileInstance.speed = projectileSpeed;
        projectileInstance.damage = 30;
        projectileInstance.team = "enemy";
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(projectileInstance.gameObject);
        yield return new WaitForSeconds(0.5f);
    }

    ////Need to switch to finite state machine below for compelxity and actually not horrible code.

    /*State currentState;
    Shooting shooting;
    Following following;

    protected override void Awake()
    {
        base.Awake();
        shooting = new Shooting(transform);
        following = new Following(transform);
    }

    void Update()
    {
        //currentState.Update();
    }

    //following finite state machine is garbage needs full refit

    private class Following : State
    {
        public Following(Transform transform) : base(transform)
        {
            
        }
        public override void Update()
        {
            
        }
    }

    private class Shooting : State
    {
        public Shooting(Transform transform) : base(transform)
        {

        }
        public override void Update()
        {

        }
    }*/
}
