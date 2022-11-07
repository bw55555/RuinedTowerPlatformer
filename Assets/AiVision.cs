using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
public class AiVision : MonoBehaviour
{
    // Idle behavior
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider;

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
    }
    // Idle behavior

    private Seeker seeker;
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;

    private void Awake()
    { 
        seeker = GetComponent < Seeker > ();
        aiPath = GetComponent < AIPath > ();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }

        






    [field: SerializeField]

    public bool PlayerDetected { get; private set; }

    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

    [Header("OverlapBox parameters")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;



    public float detectionDelay = 0.3f;

    public LayerMask detectorLayerMask;

    [Header("Gizmo parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    private GameObject target;
    public float speed;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }





    // Start is called before the first frame update
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox(
        (Vector2)detectorOrigin.position + detectorOriginOffset,
        detectorSize, 0, detectorLayerMask);

        if (collider != null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }


    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
                Gizmos.color = gizmoDetectedColor;
            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (PlayerDetected)
        {
            seeker.enabled = seeker.enabled; 
            aiPath.enabled = aiPath.enabled;
            aiDestinationSetter.enabled = aiDestinationSetter.enabled;
        }

        if (!(PlayerDetected))
        {
            seeker.enabled = !seeker.enabled;
            aiPath.enabled = !aiPath.enabled;
            aiDestinationSetter.enabled = ! aiDestinationSetter.enabled;
        }




    }
}
