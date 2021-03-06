using UnityEngine;

public class GrappleAbility : MonoBehaviour
{
    private PlayerInputProcessor playerInput;
    public CharacterAnimation anim;
    public float radius;
    public LayerMask grappleLayer;
    public bool grappling = false;
    Collider2D grappleObject = null;
    public GameObject grappleMarker;
    public LineRenderer line;
    public Transform hand;
    [SerializeField] PlayerSounds playerSounds;

    bool isOccupied = false;


    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInputProcessor>();
        playerInput.OnAbilityInvoke += GrappleAction;
    }
    private void OnDisable()
    {
        playerInput.OnAbilityInvoke -= GrappleAction;
    }

    private void Update()
    {
        if (isOccupied) return;

        if (grappling)
        {
            line.enabled = true;
            line.SetPosition(0, hand.position);
            line.SetPosition(1,  grappleObject.transform.position);
            return;

        }
        else
        {
            line.enabled = false;
        }
        if(GetPossibleObjects());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Canon"))
        {
            //playerInput.OnAbilityInvoke -= GrappleAction;
            isOccupied = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Canon"))
        {
            //playerInput.OnAbilityInvoke += GrappleAction;
            isOccupied = false;
        }
    }


    public void GrappleAction()
    {
        Debug.Log("Grappling");
        if (grappling)
        {
            anim.Grapple(false);
            Ungrapple();
        }
        else
        {
   
            Grapple();
        }
    }

    void Grapple()
    {
        if (grappleObject == null)
            return;
        grappleObject.GetComponent<GrappleObject>().Consume();
        SpringJoint2D joint = grappleObject.GetComponent<SpringJoint2D>();
        joint.connectedBody = GetComponent<Rigidbody2D>();
        grappling = true;
        playerSounds.PlayAbilitySound();
        anim.Grapple(true);
    }

    private bool GetPossibleObjects()
    {
        Collider2D[] grappleObjects = Physics2D.OverlapCircleAll(transform.position, radius, grappleLayer);
        if (grappleObjects.Length <= 0)
        {
            grappleMarker.GetComponent<SpriteRenderer>().color = Color.clear;
            return true;
        }

        float distance = 1000;
        for (int i = 0; i < grappleObjects.Length; i++)
        {
            var currentDistance = Vector2.Distance(transform.position, grappleObjects[i].transform.position);

            if (currentDistance < distance && !grappleObjects[i].GetComponent<GrappleObject>().consumed)
            {
                grappleMarker.GetComponent<SpriteRenderer>().color = Color.white;
                distance = currentDistance;
                grappleObject = grappleObjects[i];
                //grappleMarker.transform.position = grappleObject.transform.position;
                grappleMarker.transform.SetParent(grappleObject.transform);
                grappleMarker.transform.localPosition = Vector3.zero;
            }
        }

        if (grappleObject == null)
            return false;
        var direction = (grappleObject.transform.position - transform.position);
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction);
        Debug.DrawRay(transform.position, direction);
        if (raycast.collider.gameObject != grappleObject.gameObject)
        {
            grappleObject = null;
            grappleMarker.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        return false;
    }

    void Ungrapple()
    {
        SpringJoint2D joint = grappleObject.GetComponent<SpringJoint2D>();
        joint.connectedBody = null;
        grappling = false;
        grappleObject = null;
        grappleMarker.GetComponent<SpriteRenderer>().color = Color.clear;
    }

}
