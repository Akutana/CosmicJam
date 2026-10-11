using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Wormhole : MonoBehaviour
{
    [SerializeField] private GameObject wormhole;
    [SerializeField] private GameObject transition;

    Wormhole wormholeScript;
    private bool canTeleport = true;

    private void Awake()
    {
        transition.SetActive(false);
        wormholeScript = wormhole.GetComponent<Wormhole>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && wormholeScript.CanTeleport())
        {
            
            StartCoroutine(TransitionAnimation(collision));
            canTeleport = false;
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
}
