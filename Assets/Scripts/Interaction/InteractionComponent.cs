using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class InteractionComponent : MonoBehaviour
{
    protected CapsuleCollider interactionCollider;

    readonly private Collider[] _LatestInteractionColliders = new Collider[5];

    readonly private List<Collider> _ActiveInteractionColliders = new List<Collider>();

    // Start is called before the first frame update
    void OnEnable()
    {
        interactionCollider = GetComponent<CapsuleCollider>();
        if (ReferenceEquals(interactionCollider, null))
        {
            Debug.LogError("Interaction Component Owner has no Collider");
            return;
        }
    }

    public void OnInteract()
    {
        Debug.Log("Interacting");
        OverlapInteraction();
    
    }

    // Common entry point, for both overlap and active collider based interactions
    private void DistanceBasedInteraction(Collider[] colliders)
    {

        if(colliders.Length == 0)
        {
            Debug.LogWarning("No Colliders provided to Interaction function. Cannot proceed");
            return;
        }

        float BestDistance = float.MaxValue;
        int BestIndex = -1;
        for(int i = 0; i < colliders.Length; i++)
        {
            // If the Collider is still valid
            if (!ReferenceEquals(colliders[i], null))
            {
                // If this is the first index or if the Distance between the interactable and the owning object is 
                // smaller than the current best distance
                if(i == 0 || 
                    Vector3.Distance(
                        colliders[i].transform.position, 
                        transform.position) < BestDistance)
                {
                    BestIndex = i;
                    BestDistance = Vector3.Distance(
                        colliders[i].transform.position,
                        transform.position);
                }
            }
        }

        // Only perform the GetComponent function when absolutely needed and cache the result
        IInteractableInterface interactable = colliders[BestIndex].gameObject.GetComponent<IInteractableInterface>();

        if (ReferenceEquals(interactable, null))
        {
            Debug.LogWarning($"Attempting to interact with {colliders[BestIndex].gameObject}. No Components implementing IInteractableInterface found, returning");
            return;
        }

        // Attempt the interaction
        if (interactable.IsInteractable(this))
        {
            interactable.AttemptInteraction(this);
        }

    }


#region Active Collision Based Interaction

    private void OnCollisionEnter(Collision collision)
    {
        if(ReferenceEquals(collision.gameObject.GetComponent<IInteractableInterface>(), null))
        {
            Debug.LogWarning($"Attempting to add {collision.gameObject} to Active Interaction Colliders. No Components implementing IInteractableInterface found, returning");
            return;
        }

        _ActiveInteractionColliders.Add(collision.collider);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_ActiveInteractionColliders.Contains(collision.collider))
        {
            _ActiveInteractionColliders.Remove(collision.collider);
        }
    }

    #endregion

#region Overlap Based Interaction

    private void OverlapInteraction()
    {
        Debug.Log("Overlap Interaction");

        // Calculate the points in local space

        // Populates a Vector 3 with the direction of the collider
        var direction = new Vector3 { [interactionCollider.direction] = 1 };
        Debug.Log(direction);
        // Offset from the centre point = Half height of the capsule - radius of the capsule
        var offset = interactionCollider.height / 2 - interactionCollider.radius;
        // Simply add and subtract the offset from the centre, in the direction of the capsule
        var localPoint0 = interactionCollider.center - direction * offset;
        var localPoint1 = interactionCollider.center + direction * offset;

        // Convert the local space points into world space using the transform of the gameobject
        var point0 = transform.TransformPoint(localPoint0);
        var point1 = transform.TransformPoint(localPoint1);

        // Convert the local interaction radius into world space to account for scale
        var r = transform.TransformVector(interactionCollider.radius, interactionCollider.radius, interactionCollider.radius);
        // Generates the worst case scenerio radius
        var radius = Enumerable.Range(0, 3).Select(xyz => xyz == interactionCollider.direction ? 0 : r[xyz])
            .Select(Mathf.Abs).Max();


        // Collects all Colliders within the Interaction Collider, but dynamically allocates the array. DO NOT USE if anticipating a lot of calls close together
        // Collider[] overlaps = Physics.OverlapCapsule(point0, point1, radius, interactionCollider.excludeLayers);
        // DistanceBasedInteraction(overlaps);
        
        // Limited to the length of the buffer (_LatesteInteractionColliders), but is able to be called more frequently
        // Physics.OverlapCapsuleNonAlloc(point0, point1, radius, _LatestInteractionColliders, interactionCollider.excludeLayers);
        // DistanceBasedInteraction(_LatestInteractionColliders.ToArray());


    }
}
#endregion