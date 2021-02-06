using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticlePointer : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    [Range(0f, 20f)]
     public float reticleMinInnerAngle = 9f;
    [Range(0f, 20f)]
     public float reticleMinOutterAngle = 15f;
    [Range(0f, 10f)]
     public float reticleGrowthAngle = 3.4f;
    [Range(5, 30)]
    public int reticleSegments = 20;
    [Range(0f, 1f)]
    public float reticleAlpha = 1f;
    public float reticleGrowthSpeed = 8.0f;

    [Range(-32767, 32767)]
    public int reticleSortingOrder = -1;

    public Material MaterialComp { private get; set; }

    public Color color = Color.white;

    public float ReticleInnerAngle { get; private set; }
    public float ReticleOuterAngle { get; private set; }
    public float ReticleInnerDiameter { get; private set; }
    public float ReticleOuterDiameter { get; private set; }
    public float ReticleAlpha { get; private set; }

    private void OnCamInteractionEnter() {
        ReticleInnerAngle = reticleMinInnerAngle + reticleGrowthAngle;
        ReticleOuterAngle = reticleMinOutterAngle + reticleGrowthAngle;
    }
    public void OnCamInteractionExit()
    {
        ReticleInnerAngle = reticleMinInnerAngle;
        ReticleOuterAngle = reticleMinOutterAngle;
    }
    void Start()
    {
        GameEvents.current.onCamInteractionEnter += OnCamInteractionEnter;
        GameEvents.current.onCamInteractionExit += OnCamInteractionExit;

        Renderer rendererComponent = GetComponent<Renderer>();
        rendererComponent.sortingOrder = reticleSortingOrder;

        MaterialComp = rendererComponent.material;

        CreateReticleVertices();
        // transform.LookAt(target, Vector3.up);
        LookAtCamera();
        

    }

    private void Awake()
    {

        
        ReticleInnerAngle = reticleMinInnerAngle;
        ReticleOuterAngle = reticleMinOutterAngle;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDiameters(Time.time);
        
    }

    public void LookAtCamera() {
        Vector3 direction = transform.position - target.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void UpdateDiameters(float time)
    {

        if (ReticleInnerAngle < reticleMinInnerAngle)
        {
            ReticleInnerAngle = reticleMinInnerAngle;
        }

        if (ReticleOuterAngle < reticleMinOutterAngle)
        {
            ReticleOuterAngle = reticleMinOutterAngle;
        }

        float inner_half_angle_radians = Mathf.Deg2Rad * ReticleInnerAngle * 0.5f;
        float outer_half_angle_radians = Mathf.Deg2Rad * ReticleOuterAngle * 0.5f;

        float inner_diameter = 2.0f * Mathf.Tan(inner_half_angle_radians);
        float outer_diameter = 2.0f * Mathf.Tan(outer_half_angle_radians);

        ReticleInnerDiameter =
      Mathf.Lerp(ReticleInnerDiameter, inner_diameter, Time.unscaledDeltaTime * reticleGrowthSpeed);
        ReticleOuterDiameter =
      Mathf.Lerp(ReticleOuterDiameter, outer_diameter, Time.unscaledDeltaTime * reticleGrowthSpeed);

        MaterialComp.SetFloat("_InnerDiameter", ReticleInnerDiameter );
        MaterialComp.SetFloat("_OuterDiameter", ReticleOuterDiameter );
        MaterialComp.SetColor("_Color", color);
        MaterialComp.SetFloat("_Alpha", reticleAlpha);
        
    }

    private bool SetPointerTarget(bool interactive)
    {
        if (interactive)
        {
            
        }
        else
        {
            ReticleInnerAngle = reticleMinInnerAngle;
            ReticleOuterAngle = reticleMinOutterAngle;
        }

        return true;
    }

    public void FadeOut() {

    }

    private void CreateReticleVertices()
    {
        Mesh mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        GetComponent<MeshFilter>().mesh = mesh;

        int segments_count = reticleSegments;
        int vertex_count = (segments_count + 1) * 2;

#region Vertices

        Vector3[] vertices = new Vector3[vertex_count];

        const float kTwoPi = Mathf.PI * 2.0f;
        int vi = 0;
        for (int si = 0; si <= segments_count; ++si)
        {
            // Add two vertices for every circle segment: one at the beginning of the
            // prism, and one at the end of the prism.
            float angle = (float)si / (float)segments_count * kTwoPi;

            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);

            vertices[vi++] = new Vector3(x, y, 0.0f); // Outer vertex.
            vertices[vi++] = new Vector3(x, y, 1.0f); // Inner vertex.
        }
#endregion

#region Triangles
        int indices_count = (segments_count + 1) * 3 * 2;
        int[] indices = new int[indices_count];

        int vert = 0;
        int idx = 0;
        for (int si = 0; si < segments_count; ++si)
        {
            indices[idx++] = vert + 1;
            indices[idx++] = vert;
            indices[idx++] = vert + 2;

            indices[idx++] = vert + 1;
            indices[idx++] = vert + 2;
            indices[idx++] = vert + 3;

            vert += 2;
        }
#endregion

        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateBounds();
#if !UNITY_5_5_OR_NEWER
        // Optimize() is deprecated as of Unity 5.5.0p1.
        mesh.Optimize();
#endif  // !UNITY_5_5_OR_NEWER
    }
}


// Documento de identidad por ambas caras (utilizar una sola página)
// Formulario de afiliación firmado (utilizar una sola página)
// Formato de autorización de tratamiento de datos diligenciadas y firmadas por el trabajador
// Afiliación o certificación de la EPS como cotizante o copia del último pago realizado (debe corresponder al mes inmediatamente anterior o actual al momento de la afiliación). Si en el pago no aparece el nombre de la EPS diligencie a mano sobre el recibo el nombre de ésta, en la parte superior del soporte.
// Afiliación o certificación de la AFP como cotizante o copia del último pago realizado (debe corresponder al mes inmediatamente anterior o actual al momento de la afiliación). Si en el pago no aparece el nombre de la AFP diligencie a mano sobre el recibo el nombre de ésta, en la parte superior del soporte.
// Copia del contrato de prestación de servicios o documento que contenga tiempo, modo y lugar del contrato a realizar, debidamente firmado por el contratante y el contratista.
