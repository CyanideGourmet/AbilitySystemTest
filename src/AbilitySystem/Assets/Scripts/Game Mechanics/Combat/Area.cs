using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : DamageMedium
{
    float areaRadiusModifier = 1f;
    float deactivateTime = 1f;

    List<Collider2D> alreadyDamaged = new List<Collider2D>();

    private void OnEnable()
    {
        StartCoroutine(Deactivate(deactivateTime));
        Collider2D[] insideTheTrigger = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * transform.localScale.x, LayerMask.GetMask("Object"));
        foreach (Collider2D collider in insideTheTrigger) DoDamage(collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DoDamage(collision);
    }

    private void DoDamage(Collider2D collision)
    {
        if (!alreadyDamaged.Contains(collision) && DamagePacket.source.transform.parent.parent.gameObject != collision.gameObject)
        {
            alreadyDamaged.Add(collision);
            DamageReciever damageReciever = collision.GetComponentInChildren<DamageReciever>();
            if (damageReciever != null) damageReciever.Recieve(DamagePacket);
        }
    }

    public void ChangeParameters(float radius, float time)
    {
        transform.localScale = transform.localScale / areaRadiusModifier;
        areaRadiusModifier = radius;
        transform.localScale = transform.localScale * areaRadiusModifier;
        deactivateTime = time;
    }

    IEnumerator Deactivate(float time)
    {
        while ((time -= Time.deltaTime) > 0f) yield return null;
        alreadyDamaged.Clear();
        gameObject.SetActive(false);
        yield return null;
    }
}
