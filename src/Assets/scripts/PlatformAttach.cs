using System.Collections;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    private Transform platformParent;

    private void Awake()
    {
        platformParent = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (player != null)
        {
            StartCoroutine(SetParentLater(player.transform, platformParent));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (player != null)
        {
            StartCoroutine(SetParentLater(player.transform, null));
        }
    }

    private IEnumerator SetParentLater(Transform playerTransform, Transform newParent)
    {
        yield return null;

        if (playerTransform != null)
        {
            playerTransform.SetParent(newParent);
        }
    }
}