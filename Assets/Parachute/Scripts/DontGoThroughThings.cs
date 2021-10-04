using UnityEngine;
using System.Collections;
using System.Linq;

public class DontGoThroughThings : MonoBehaviour
{
    public enum TriggerTarget
    {
        None = 0,
        Self = 1,
        Other = 2,
        Both = 3
    }

    public LayerMask hitLayers = -1;
    public string MessageName = "OnTriggerEnter2D";
    public TriggerTarget triggerTarget = TriggerTarget.Both;
    public float momentumTransferFraction = 0;
    private float minimumExtent;
    private float sqrMinimumExtent;
    private Vector2 previousPosition;
    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;

    //initialize values 
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponents<Collider2D>().FirstOrDefault();
        if (myCollider == null || myRigidbody == null)
        {
            Debug.LogError("ProjectileCollisionTrigger2D is missing Collider2D or Rigidbody2D component", this);
            enabled = false;
            return;
        }

        previousPosition = myRigidbody.transform.position;
        minimumExtent = Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }

    void FixedUpdate()
    {
        //have we moved more than our minimum extent? 
        var origPosition = transform.position;
        Vector2 movementThisStep = (Vector2)transform.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

            //check for obstructions we might have missed 
            RaycastHit2D[] hitsInfo = Physics2D.RaycastAll(previousPosition, movementThisStep, movementMagnitude, hitLayers.value);

            //Going backward because we want to look at the first collisions first. Because we want to destroy the once that are closer to previous position
            for (int i = 0; i < hitsInfo.Length; ++i)
            {
                var hitInfo = hitsInfo[i];
                if (hitInfo && hitInfo.collider != myCollider)
                {
                    // apply force
                    if (hitInfo.rigidbody && momentumTransferFraction != 0)
                    {
                        var dv = myRigidbody.velocity;
                        var m = myRigidbody.mass;
                        var dp = dv * m;
                        var impulse = momentumTransferFraction * dp;
                        hitInfo.rigidbody.AddForceAtPosition(impulse, hitInfo.point, ForceMode2D.Impulse);

                        if (momentumTransferFraction < 1)
                        {
                            // also apply force to self (in opposite direction)
                            var impulse2 = (1 - momentumTransferFraction) * dp;
                            hitInfo.rigidbody.AddForceAtPosition(-impulse2, hitInfo.point, ForceMode2D.Impulse);
                        }
                    }

                    // move this object to the point of collision
                    transform.position = hitInfo.point;

                    // send hit message
                    if (((int)triggerTarget & (int)TriggerTarget.Other) != 0 && hitInfo.collider.isTrigger)
                    {
                        hitInfo.collider.SendMessage(MessageName, myCollider, SendMessageOptions.DontRequireReceiver);
                    }
                    if (((int)triggerTarget & (int)TriggerTarget.Self) != 0)
                    {
                        SendMessage(MessageName, hitInfo.collider, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }

        previousPosition = transform.position = origPosition;
    }
}