using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Wormhole : MonoBehaviour
{
    [SerializeField] private GameObject wormhole;
    [SerializeField] private GameObject transition;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private Color cooldownColor;

    Wormhole wormholeScript;
    private bool canTeleport = true;
    private bool isInCooldown = false;

    private void Awake()
    {
        transition.SetActive(false);
        wormholeScript = wormhole.GetComponent<Wormhole>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")
            && wormholeScript.CanTeleport()
            && !wormholeScript.IsInCooldown()
            && !IsInCooldown())
        {
            
            StartCoroutine(TransitionAnimation(collision));
            canTeleport = false;
            StartCoroutine(OnTeleport());
        }
    }

    IEnumerator TransitionAnimation(Collider2D collision)
    {
        transition.SetActive(true);

        yield return new WaitForSeconds(0.58f);

        collision.transform.position = wormhole.transform.position;

        yield return new WaitForSeconds(0.58f);
        Debug.Log("teleported!");
        transition.SetActive(false);
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
