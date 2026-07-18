using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private const string PlayerTag = "Player";

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag)) return;

        var activeWeapon = other.GetComponentInChildren<ActiveWeapon>();

        OnPickup(activeWeapon);
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
