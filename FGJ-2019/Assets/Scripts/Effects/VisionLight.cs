// Date   : 11.08.2018 11:41
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionLight : MonoBehaviour {
    private float viewRadius;
    public float ViewRadius { get { return viewRadius; } }

    [SerializeField]
    private bool drawDebug = false;

    [SerializeField]
    private bool drawTargetDebug = false;

    [SerializeField]
    [Range(0, 360)]
    private float viewAngle;
    public float ViewAngle { get { return viewAngle; } }

    [SerializeField]
    private float targetFindInterval = 1f;

    [SerializeField]
    private LayerMask targetMask;

    [SerializeField]
    private LayerMask obstacleMask;

    [SerializeField]
    private float meshResolution = 25f;

    [SerializeField]
    private MeshFilter viewMeshFilter;
    private Mesh viewMesh;
    private MeshCollider viewMeshCollider;

    [SerializeField]
    private int edgeResolveIterations = 5;

    [SerializeField]
    private float edgeDistanceTreshold = 1f;

    [SerializeField]
    private float radius = 5;

    private List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> VisibleTargets { get { return visibleTargets; } }

    private static string PROPERTY_RADIUS = "radius";
    private static string PROPERTY_ANGLE = "angle";
    private static string PROPERTY_ROTATION = "rotation";

    private GridObject gridObject;

    private bool started = false;

    public void Init() {
        gridObject = GetComponent<GridObject>();
        float radiusFromMap = gridObject.GetFloatProperty(PROPERTY_RADIUS);
        if (radiusFromMap > -1) {
            radius = radiusFromMap;
        }
        float angleFromMap = gridObject.GetFloatProperty(PROPERTY_ANGLE);
        if (angleFromMap > -1) {
            viewAngle = angleFromMap;
        }
        float rotationFromMap = gridObject.GetFloatProperty(PROPERTY_ROTATION);
        if (rotationFromMap > -1) {
            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.y = rotationFromMap;
            transform.localEulerAngles = currentRotation;
        }
        viewRadius = radius;
        //transform.localPosition = new Vector3(x, y, 0);
        StartCoroutine("FindTargetsWithDelay", targetFindInterval);
        viewMesh = new Mesh
        {
            name = "View Mesh"
        };
        viewMeshFilter.mesh = viewMesh;
        viewMeshCollider = viewMeshFilter.GetComponent<MeshCollider>();
        started = true;
        Vector3 newPosition = transform.position;
        newPosition.z = -0.2f;
        transform.position = newPosition;
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void Update () {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (started) {
            DrawFieldOfView();
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i += 1)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            if (drawDebug) {
                Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.cyan);
            }
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDistanceTresholdExceeded = Mathf.Abs(oldViewCast.Distance - newViewCast.Distance) > edgeDistanceTreshold;
                if (oldViewCast.Hit != newViewCast.Hit || (oldViewCast.Hit && newViewCast.Hit && edgeDistanceTresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.PointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.PointA);
                    }
                    if (edge.PointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.PointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.Point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        List<Vector3> pointsForLineRenderer = new List<Vector3>
        {
            Vector3.zero
        };
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i += 1)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
        viewMeshCollider.sharedMesh = viewMesh;
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.Angle;
        float maxAngle = maxViewCast.Angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;
        for (int i = 0; i < edgeResolveIterations; i += 1)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDistanceTresholdExceeded = Mathf.Abs(minViewCast.Distance - newViewCast.Distance) > edgeDistanceTreshold;
            if (minViewCast.Hit != newViewCast.Hit || (minViewCast.Hit && newViewCast.Hit && edgeDistanceTresholdExceeded))
                if (newViewCast.Hit == minViewCast.Hit)
                {
                    minAngle = angle;
                    minPoint = newViewCast.Point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.Point;
                }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + direction * viewRadius, viewRadius, globalAngle);
        }
    }

    struct ViewCastInfo
    {
        private bool hit;
        public bool Hit { get { return hit; } }
        private Vector3 point;
        public Vector3 Point { get { return point; } }
        private float distance;
        public float Distance { get { return distance; } }
        private float angle;
        public float Angle { get { return angle; } }

        public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }

    struct EdgeInfo
    {
        private Vector3 pointA;
        public Vector3 PointA { get { return pointA; } }
        private Vector3 pointB;
        public Vector3 PointB { get { return pointB; } }

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    float targetFindAngleBuffer = 5f;

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i += 1)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, directionToTarget) < viewAngle / 2 + targetFindAngleBuffer)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    if (drawTargetDebug) {
                        Debug.DrawLine(transform.position, target.position, Color.green, targetFindInterval);
                    }
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

}
