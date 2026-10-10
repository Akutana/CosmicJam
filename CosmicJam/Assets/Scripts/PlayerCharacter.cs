using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 90f;

    private void Update()
    {
        int children = transform.childCount;

        for (int i = 0; i < children; ++i)
        {
            Transform obj =  transform.GetChild(i);

            obj.RotateAround(
                transform.position,
                Vector3.forward,
                -orbitSpeed * Time.deltaTime
            );
        }
    }
}
