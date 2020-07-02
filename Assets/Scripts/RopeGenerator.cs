using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    private Rigidbody2D hook;
    public GameObject linkPrefab;
    public int links_number;

    private void Start()
    {
        hook = GetComponent<Rigidbody2D>();
        GenerateRope();
    }

    private void GenerateRope()
    {
        Rigidbody2D previousRB = hook;
        for (int i = 1; i <= links_number; i++)
        {
            GameObject link = Instantiate(linkPrefab, previousRB.transform.GetChild(0).transform.position, Quaternion.identity);
            link.transform.SetParent(hook.transform);
            link.GetComponent<HingeJoint2D>().connectedAnchor = previousRB.transform.GetChild(0).transform.position;
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;
            previousRB = link.GetComponent<Rigidbody2D>();
        }
    }
}
