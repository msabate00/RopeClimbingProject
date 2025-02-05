using System.Collections;
using UnityEngine;

public class CustomWaterSplash : MonoBehaviour
{
    public Vector2 localPosition = new Vector2(0, 0); // Coordenadas locales
    public float minForce = 0.1f;
    public float maxForce = 0.5f;
    public float interval = 0.2f;
    public float gizmoRadius = 0.5f;
    public Color gizmoColor = Color.cyan;

    private InteractableWater _water;
    private Coroutine _splashCoroutine;

    private void Awake()
    {
        _water = GetComponent<InteractableWater>();
    }

    void Start()
    {
        _splashCoroutine = StartCoroutine(ContinuousSplash());
    }

    private IEnumerator ContinuousSplash()
    {
        Vector2 worldPosition = transform.TransformPoint(localPosition); // Convertir a coordenadas mundiales

        while (true) // Loop infinito para splashes constantes
        {
            float randomForce = Random.Range(minForce, maxForce); // Nueva fuerza aleatoria en cada iteración
            _water.SplashAt(worldPosition, randomForce);
            yield return new WaitForSeconds(interval); // Espera el tiempo especificado
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Vector3 worldPosition = transform.TransformPoint(localPosition);
        Gizmos.DrawWireSphere(worldPosition, gizmoRadius);
    }
}
