using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public Transform bg1;
    public Transform bg2;
    public float parallaxFactor = 0.5f; 
    private float width;
    private float startCamX;
    private Vector3 bg1StartPos;
    private Vector3 bg2StartPos;

    void Start()
    {
        if (cam == null) 
        {
            cam = Camera.main.transform;
        }

        var sr = bg1.GetComponent<SpriteRenderer>();
        width = sr.bounds.size.x;
        startCamX = cam.position.x;
        bg1StartPos = bg1.position;
        bg2StartPos = bg2.position;
        bg2.position = new Vector3(bg1.position.x + width, bg2.position.y, bg2.position.z);
    }

    void Update()
    {
        if (cam == null)
        {
            return;
        }

        float camDeltaX = cam.position.x - startCamX;
        float targetX = camDeltaX * parallaxFactor;
        bg1.position = new Vector3(bg1StartPos.x + targetX, bg1StartPos.y, bg1.position.z);
        bg2.position = new Vector3(bg2StartPos.x + targetX, bg2StartPos.y, bg2.position.z);
        float camX = cam.position.x;

        if (camX - bg1.position.x > width)
        {
            bg1.position = new Vector3(bg2.position.x + width, bg1.position.y, bg1.position.z);
            bg1StartPos = bg1.position;
        }

        if (camX - bg2.position.x > width)
        {
            bg2.position = new Vector3(bg1.position.x + width, bg2.position.y, bg2.position.z);
            bg2StartPos = bg2.position;
        }
    }
}
