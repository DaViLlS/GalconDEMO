using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicShip : Ship
{
    [SerializeField] private float Shipspeed;

    public Vector3 Target { get; set; }

    private void Update()
    {
        float rotateZ = Mathf.Atan2(Target.y - transform.position.y, Target.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

        transform.position = Vector3.MoveTowards(transform.position, Target, Shipspeed * Time.deltaTime);
    }

    public void SentShipToTarget()
    {
        gameObject.SetActive(true);
    }

    public void DestroyShip()
    {
        Destroy(gameObject);
    }
}
