using UnityEngine;

public class HookGenerator : MonoBehaviour
{
    GlobalInfo global;
    GameObject player;
    public Rigidbody2D hook;
    public GameObject linkPrefab;
    public int links_number;
    public float seconds;
    GameObject last_link;

    private void Start()
    {
        global = GameObject.Find("info").GetComponent<GlobalInfo>();
        player = GameObject.Find("Player");

        float distance = Vector2.Distance(transform.position, player.transform.position);
        links_number = (int)(distance / 0.28f);

        float angle = Vector2.Angle(player.transform.position - hook.transform.position, -hook.transform.up);
        hook.transform.rotation = Quaternion.Euler(0, 0, angle);
        Rigidbody2D previousRB = hook;
        for (int i = 1; i <= links_number; i++)
        {
            GameObject link = Instantiate(linkPrefab, previousRB.transform.GetChild(0).transform.position, Quaternion.identity);
            link.transform.SetParent(hook.transform);
            angle = Vector2.Angle(player.transform.position - link.transform.position, -link.transform.up);
            if (player.transform.position.x < link.transform.position.x) angle *= -1;
            link.transform.rotation = Quaternion.Euler(0,0, angle);
            link.transform.position = new Vector3(link.transform.position.x, link.transform.position.y, 0f);
            link.GetComponent<HingeJoint2D>().connectedAnchor = previousRB.transform.GetChild(0).transform.position;
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;
            previousRB = link.GetComponent<Rigidbody2D>();
            if (i == links_number) last_link = link;
        }
    }

    private void Update()
    {
        if (last_link)
        {
            last_link.GetComponent<Rigidbody2D>().MovePosition(player.transform.position);
            if (Vector2.Distance(last_link.transform.position, player.transform.position) < 1.5f)
            {
                player.GetComponent<DistanceJoint2D>().enabled = true;
                player.GetComponent<DistanceJoint2D>().connectedBody = last_link.GetComponent<Rigidbody2D>();
                last_link = null;
                global.active_hook = hook.gameObject;
            }
        }
    }
}
