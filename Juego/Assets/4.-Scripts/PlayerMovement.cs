using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  
    public static PlayerMovement instnacie;
    public Transform cam;
     
    public GameObject Player;
    public float speed;
    [SerializeField]private float DashVelocity;
     [SerializeField]   private Rigidbody rgbd;
    public List<GameObject> Bulls;
    [SerializeField]private GameObject prefab;

    public Vector3 worldPosition;
    //public GameObject GuardBulls;

    public float angulo;
    private float X;
    private float Y;
    [HideInInspector]
    public float posX;
    [HideInInspector]
    public float posZ;

    void Start()
    {
   
        if (instnacie == null)
        {
            instnacie = this;
        }
        rgbd = GetComponent<Rigidbody>();

        //DIstancia = Vector3.Distance(cam.transform.position, transform.position);
      
        //print(DIstancia);
    }

    // Update is called once per frame
    void Update()
    {
        cam.position = new Vector3(transform.position.x - 17.4f, cam.position.y, transform.position.z - 27f);
        Vector3 actualPos = transform.position;
          float MovX;
           float MovY;
           MovX = Input.GetAxis("Horizontal");
           MovY = Input.GetAxis("Vertical");

          rgbd.velocity = new Vector3(MovX*speed, 0, MovY * speed);


        /* Vector3 Mousepos = -Vector3.one;
         Mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, DIstancia));
         // print(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x+0 , Input.mousePosition.y + 0, Input.mousePosition.z + DIstancia)));
       //  print(Mousepos.y);
         //  print(transform.position.y-Mousepos.y);
         Mousepos.y = 0;
         MouseDiference = Mousepos-transform.position;
         */

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "Floor")
        {
            worldPosition = hitData.point;
        }

        if (Input.GetKeyDown(KeyCode.Space))
         {

             Vector3 temp = new Vector3(MovX, 0, MovY * speed).normalized;
             Vector3 Direction = temp - transform.position;

             rgbd.AddForce((worldPosition - transform.position).normalized * DashVelocity, ForceMode.Impulse);
         }
         Vector3 mousePos = worldPosition;

        /*Vector3 playerPos = cam.WorldToScreenPoint(transform.position);
        Vector3 dir = worldPosition - playerPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        print(-angle);
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        */
        Y = worldPosition.z - transform.position.z;
        X = worldPosition.x - transform.position.x;

        angulo = Mathf.Atan2(Y, X) * Mathf.Rad2Deg;

        posX = 10 * Mathf.Cos(angulo * Mathf.Deg2Rad);
        posZ = 10 * Mathf.Sin(angulo * Mathf.Deg2Rad);

        transform.rotation = Quaternion.Euler(0, transform.rotation.y - angulo, 0);


        if (Input.GetMouseButtonDown(0))
        {
            if (Bulls.Count<=20)
            {
                GameObject bul = Instantiate(prefab, transform.position, transform.rotation);

                bul.GetComponent<BulletPlayer>().speedX = posX;
                bul.GetComponent<BulletPlayer>().speedZ = posZ;
                //Bulls.Add(bul);
            }
          
        }

       


    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(worldPosition, 1f);
    }
}
