using UnityEngine;

/// <summary>
/// Sahne dosyasi olmadan calisan test sahnesi.
/// RuntimeInitializeOnLoadMethod sayesinde bos bir sahnede bile calisir.
/// </summary>
public static class Boot
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        Debug.Log("[RideX] Boot calisti");

        var cameraGo = new GameObject("Main Camera");
        var cam = cameraGo.AddComponent<Camera>();
        cam.transform.position = new Vector3(0f, 4.5f, 9f);
        cam.transform.LookAt(new Vector3(0f, 1f, 0f));
        cameraGo.tag = "MainCamera";

        var lightGo = new GameObject("Sun");
        var light = lightGo.AddComponent<Light>();
        light.type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(48f, 32f, 0f);

        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(8f, 1f, 8f);
        ground.GetComponent<Renderer>().material.color = new Color(0.20f, 0.21f, 0.23f);

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "TestCube";
        cube.transform.position = new Vector3(0f, 1f, 0f);
        cube.GetComponent<Renderer>().material.color = new Color(1f, 0.35f, 0.12f);
        cube.AddComponent<Spinner>();

        Debug.Log($"[RideX] Sahne hazir. Ekran: {Screen.width}x{Screen.height} DPI:{Screen.dpi}");
    }
}

public class Spinner : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0f, 40f * Time.deltaTime, 0f);
    }
}
