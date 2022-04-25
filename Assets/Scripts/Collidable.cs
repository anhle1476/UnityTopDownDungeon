using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Collidable : MonoBehaviour
{
    public bool collidable = true;
    public ContactFilter2D contactFilter;
    private BoxCollider2D boxCollider;
    private readonly Collider2D[] hits = new Collider2D[10]; // limit the number of hits to be 10 at a time

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        if (collidable)
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

        SpecificUpdate();
    }

    /// <summary>
    /// Call after the the collide checking logic in <see cref="Update"/> method
    /// </summary>
    protected virtual void SpecificUpdate()
    {

    }

    protected abstract void OnCollide(Collider2D collider);

}
