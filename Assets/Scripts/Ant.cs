using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ant : MonoBehaviour
{
    [SerializeField]
    private AnimatedSprite deathSequence;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Movement movement;
    public bool isImmune { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }
        else
        {
            movement.SetDirection(Vector2.zero, true);
        }

        // Rotate 
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        circleCollider.enabled = true;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.Restart();
    }

    public void SpeedBoost()
    {
        StopCoroutine(nameof(SpeedBoostTimer));
        StartCoroutine(SpeedBoostTimer());
    }

    private IEnumerator SpeedBoostTimer()
    {
        // 2x speed for 5 seconds
        movement.speedMultiplier = 2f;

        yield return new WaitForSeconds(5f);

        // Back to normal
        movement.speedMultiplier = 1f;
    }

    public void ImmunityBoost()
    {
        StopCoroutine(nameof(ImmunityTimer));
        StartCoroutine(ImmunityTimer());
    }

    private IEnumerator ImmunityTimer()
    {
        isImmune = true;

        yield return new WaitForSeconds(5f);

        isImmune = false;
    }
}
