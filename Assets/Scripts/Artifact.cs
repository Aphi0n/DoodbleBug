using UnityEngine;

public class Artifact : MonoBehaviour
{
    public enum ArtifactType
    {
        ExtraLife,
        SpeedBoost,
        Immunity,
        Key
    }

    public ArtifactType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if Ant touched the artifact
        Ant player = other.GetComponent<Ant>();

        if (player == null)
            return;


        switch (type)
        {
            case ArtifactType.ExtraLife:
                GameManager gameManager =
                    Object.FindAnyObjectByType<GameManager>();

                if (gameManager != null)
                {
                    gameManager.AddLife();
                }
                break;

            case ArtifactType.SpeedBoost:
                player.SpeedBoost();
                break;

            case ArtifactType.Immunity:
                player.ImmunityBoost();
                break;

            case ArtifactType.Key:
                GameManager manager =
                    Object.FindAnyObjectByType<GameManager>();

                if (manager != null)
                {
                    manager.AddKey();
                }
                break;
        }

        Destroy(gameObject);
    }
}