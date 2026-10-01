using UnityEngine;

public class GhostChase : GhostBehavior
{
    // Chase forever — ignore the duration timer
    public override void Enable(float duration)
    {
        enabled = true;
        CancelInvoke();

        // Allow Ghost to pass through walls
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Ghost"),
            LayerMask.NameToLayer("Wall"),
            true
        );
    }

    private void Update()
    {
        if (ghost.target == null)
            return;

        // Calculate direction directly toward the Ant
        Vector2 direction =
            ((Vector2)ghost.target.position -
             (Vector2)transform.position).normalized;

        // Force movement toward the Ant
        ghost.movement.SetDirection(direction, true);
    }

    public override void Disable()
    {
        base.Disable();

        // Turn wall collision back on if Chase is ever disabled
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Ghost"),
            LayerMask.NameToLayer("Wall"),
            false
        );
    }
}