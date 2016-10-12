using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour
{

    RaycastHit hit_man;
    Ray ray_garvey;
    public GameObject Building;


    // Update is called once per frame
    void Update()
    {

        ray_garvey = Camera.main.ScreenPointToRay(Input.mousePosition);

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Collider>().Raycast(ray_garvey, out hit_man, Mathf.Infinity) && Input.GetMouseButtonDown(0))
            {
                // hit_man.transform.child
                //Debug.Log("Hit on: " + hit_man.transform.gameObject.name);
                if (hit_man.transform.childCount > 0)
                {
                    foreach (Transform Cc in hit_man.transform)
                    {
                        Destroy(Cc.gameObject);
                    }
                }
                else
                {
                    GameObject building = (GameObject)Instantiate(Building, hit_man.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, hit_man.transform);
                }
                break;

            }
        }

    }
}
