using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;
    public float speed = 20;
    private Turret sourceTurret;
    private Transform target;
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    public void SetSourceTurret(Turret _sourceTurret)
    {
        this.sourceTurret = _sourceTurret;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage(damage, sourceTurret.gameObject);
            Destroy(this.gameObject);
        }
    }
}
