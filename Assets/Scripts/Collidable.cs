using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Collidable : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    private BoxCollider2D boxCollider;
    private readonly Collider2D[] hits = new Collider2D[10]; // limit the number of hits to be 10 at a time

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        boxCollider.OverlapCollider(contactFilter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            // free the collider array
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D collider)
    {
        Debug.Log(collider.name);
    }

}
