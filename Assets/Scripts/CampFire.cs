using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    public int healing;
    public float healingRate;

    private Collider parentCollider;
    private Collider childCollider;
    private Collider[] colliders;

    List<IDamageable> things = new List<IDamageable>();

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();

        parentCollider = GetComponent<Collider>();
        childCollider = colliders.FirstOrDefault(col => col != parentCollider);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    void Heal()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].GetHeal(healing);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (other.bounds.Intersects(parentCollider.bounds))
            {
                things.Add(damageable);
                childCollider.enabled = false;
                InvokeRepeating("DealDamage", 0, damageRate);
                CancelInvoke("Heal");
            }
            else if (other.bounds.Intersects(childCollider.bounds))
            {
                things.Add(damageable);
                InvokeRepeating("Heal", 0, healingRate);
                CancelInvoke("DealDamage");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (things.Contains(damageable))
                things.Remove(damageable);

            if(!other.bounds.Intersects(childCollider.bounds))
            {
                CancelInvoke("Heal");
            }

            if (!other.bounds.Intersects(parentCollider.bounds) && childCollider.enabled == false)
            {
                CancelInvoke("DealDamage");
                childCollider.enabled = true;
            }

        }
    }

}
