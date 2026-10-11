using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    [SerializeField] private GameObject wormhole;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private Color cooldownColor;

    Wormhole wormholeScript;
    private bool canTeleport = true;
    private bool isInCooldown = false;

    private void Awake()
    {
        wormholeScript = wormhole.GetComponent<Wormhole>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")
            && wormholeScript.CanTeleport()
            && !wormholeScript.IsInCooldown()
            && !IsInCooldown())
        {
            collision.transform.position = wormhole.transform.position;
            canTeleport = false;
            StartCoroutine(OnTeleport());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wormholeScript.SetCanTeleport(true);
        }
    }

    public bool CanTeleport()
    {
        return canTeleport;
    }

    public void SetCanTeleport(bool value)
    {
        canTeleport = value;
    }

    public bool IsInCooldown()
    {
        return isInCooldown;
    }

    private IEnumerator OnTeleport()
    {
        wormholeScript.isInCooldown = true;
        
        SpriteRenderer sprite = wormholeScript.GetComponent<SpriteRenderer>();
        Color baseColor = sprite.color;

        sprite.color = cooldownColor;

        //float elapsed = 0f;
        //bool isBaseColor = true;

        //while (elapsed < cooldownDuration)
        //{
        //    //isBaseColor == true ? sprite.color = cooldownColor : sprite.color = baseColor;

        //    if (isBaseColor)
        //        sprite.color = cooldownColor;
        //    else
        //        sprite.color = baseColor;

        //    isBaseColor = !isBaseColor;

        //    yield return new WaitForSeconds(.5f);

        //    elapsed += .5f;
        //}

        yield return new WaitForSeconds(cooldownDuration);

        sprite.color = baseColor;

        wormholeScript.isInCooldown = false;
    }
}
