using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject settingsInstance;
    private int roomSize;
    private int houseSize;

    void Start()
    {
        var cam = transform.GetComponent(typeof(Camera)) as Camera;
        var camscript = transform.GetComponent(typeof(DragCamera2D)) as DragCamera2D;
        var settings = settingsInstance.GetComponent(typeof(Settings)) as Settings;
        roomSize = settings.roomSize;
        houseSize = settings.houseSize;
        float posx = ((float)(roomSize * houseSize + houseSize - 1) / 2) - 1.5F;
        float posy = houseSize * roomSize;
        cam.transform.position = new Vector3(posx, posy, posx);
        cam.orthographicSize = posy;
        camscript.maxZoom = posy;
    }
}