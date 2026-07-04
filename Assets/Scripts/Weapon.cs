using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private StarterAssetsInputs _starterAssetsInputs;
    private Camera _camera;

    private void Awake()
    {
        _starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

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
        if (!_starterAssetsInputs.shoot) return;

        bool isHit = Physics.Raycast
        (
            _camera.transform.position,
            _camera.transform.forward,
            out RaycastHit hit,
            Mathf.Infinity
        );

        if (isHit)
        {
            Debug.Log(hit.transform.name);
        }

        _starterAssetsInputs.ShootInput(false);
    }
}
