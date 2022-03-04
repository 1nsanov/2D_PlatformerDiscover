using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    private Collider2D _groundCollider;

    public bool IsTouchingLayers;

    private void Awake()
    {
        _groundCollider = GetComponent<Collider2D>();  
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsTouchingLayers = _groundCollider.IsTouchingLayers(_groundLayer);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTouchingLayers = _groundCollider.IsTouchingLayers(_groundLayer);
    }
}
