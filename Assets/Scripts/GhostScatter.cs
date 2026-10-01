using UnityEngine;
using System.Collections.Generic;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        // After Scatter finishes, start Chase
        if (ghost != null && ghost.chase != null)
        {
            ghost.chase.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ChooseNewDirection();
        }
    }

    private void ChooseNewDirection()
    {
        Vector2 current = ghost.movement.direction;
        Vector2 reverse = -current;

        Vector2[] directions =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        List<Vector2> choices = new List<Vector2>();

        foreach (Vector2 dir in directions)
        {
            if (dir == current)
                continue;

            if (!ghost.movement.Occupied(dir))
            {
                choices.Add(dir);
            }
        }

        if (choices.Count > 0)
        {
            Vector2 newDirection =
                choices[Random.Range(0, choices.Count)];

            ghost.movement.SetDirection(newDirection, true);
        }
        else
        {
            ghost.movement.SetDirection(reverse, true);
        }
    }
}