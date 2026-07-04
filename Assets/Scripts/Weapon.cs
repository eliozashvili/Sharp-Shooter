using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        HandleRaycast();
    }

    private void HandleRaycast()
    {
        bool isHit = Physics.Raycast
        (
            _camera.transform.position,
            _camera.transform.forward,
            out RaycastHit hit,
            Mathf.Infinity
        );

        if (isHit)
            Debug.Log(hit.transform.name);
    }
}
