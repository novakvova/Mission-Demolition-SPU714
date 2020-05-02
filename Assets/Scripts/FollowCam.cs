using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;
    [Header("Set Dynamically")]
    public float camZ;
    
    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void Update()
    {
        
    }
    
    void FixedUpdate()
    {
         //Однострочная версия if не требует фигурных скобок
        if (POI == null) return;
        // Получить позицию интересующего объекта
        Vector3 destination = POI.transform.position; 
        
        // Принудительно установить значение destination.z равным camZ, чтобы
        // отодвинуть камеру подальше
        destination.z = camZ;
        // Поместить камеру в позицию destination
        transform.position = destination;
        //Camera.main.orthographicSize = destination.y + 10;
    }
}
