using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // The main board that the camera rotates around
    public GameObject board;
    private const float ROTATE_SPEED = 100.0f;
    private const float MAX_RAYCAST_DIST = 1000f;
    private const string BOX_TAG = "Box";

    private void DeselectAllBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag(BOX_TAG);
        foreach (GameObject box in boxes)
        {
            box.GetComponent<Outline>().enabled = false;
        }
    }
    private void SelectBox(GameObject box)
    {
        DeselectAllBoxes();
        Outline outline = box.GetComponent<Outline>();
        outline.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Pressing A or LeftArrow -> Rotate the camera left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.RotateAround(board.transform.position, Vector3.up, ROTATE_SPEED * Time.deltaTime);
        }

        // Pressing D or RightArrow -> Rotate the camera right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.RotateAround(board.transform.position, -Vector3.up, ROTATE_SPEED * Time.deltaTime);
        }


        // Pressing mouse 1
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hits = Physics.RaycastAll(ray, MAX_RAYCAST_DIST).Where(hit => hit.transform.gameObject.CompareTag(BOX_TAG)).ToArray();
            if (hits.Length > 0) // Hit a "Box" -> Select it
            {
                System.Array.Sort(hits, (hit1, hit2) => hit1.distance < hit2.distance ? -1 : 1);
                SelectBox(hits[0].transform.gameObject);
            }
            else // Clicked on nothing -> Deselect selected box
            {
                DeselectAllBoxes();
            }
        }
    }
}
