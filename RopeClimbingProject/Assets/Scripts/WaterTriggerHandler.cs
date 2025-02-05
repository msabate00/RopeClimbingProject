using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerHandler : MonoBehaviour
{

    [SerializeField] private LayerMask _waterMask;
    [SerializeField] private Transform _splashParticles;

    private EdgeCollider2D _edgeCollider;
    private InteractableWater _water;

    private void Awake()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _water = GetComponent<InteractableWater>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((_waterMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                //spawn particles




                int multiplier = 1;
                if(rb.velocity.y < 0)
                {
                    multiplier = -1;
                }
                else
                {
                    multiplier = 1;
                }

                float vel = rb.velocity.y * _water.forceMultiplier;
                vel = Mathf.Clamp(Mathf.Abs(vel), 0, _water.maxForce);
                vel *= multiplier;

                _water.Splash(collision, vel);


            }
        }
    }
}
