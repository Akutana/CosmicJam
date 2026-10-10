using UnityEngine;

public class OrbitalZone : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();

        if (!obj.transform.parent.transform.IsChildOf(player.transform))
        {
            obj.transform.parent.transform.parent = player.transform;
        }
    }
}
