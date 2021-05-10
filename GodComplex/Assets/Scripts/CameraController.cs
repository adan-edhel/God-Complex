using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    CinemachineFreeLook freelook;
    CinemachineFreeLook.Orbit[] originalOrbits;

    [Range(0.3f, 1f)]
    public float minScale = 0.4f;

    [Range(1f, 5f)]
    public float maxScale = 5f;

    public float movementSpeed;
    public float movementTime;

    public Vector3 newPosition;

    public AxisState zAxis = new AxisState(0, 1, false, true, 50f, 0.1f, 0.1f, "", false);

    bool movingCamera;
    public bool rotatingCamera;

    Vector3 moveInput;

    private void OnValidate()
    {
        minScale = Mathf.Max(0.01f, minScale);
        maxScale = Mathf.Max(minScale, maxScale);
    }

    private void Awake()
    {
        Instance = this;

        freelook = GetComponentInChildren<CinemachineFreeLook>();

        if (freelook)
        {
            originalOrbits = new CinemachineFreeLook.Orbit[freelook.m_Orbits.Length];
            for (int i = 0; i < freelook.m_Orbits.Length; i++)
            {
                originalOrbits[i].m_Height = freelook.m_Orbits[i].m_Height;
                originalOrbits[i].m_Radius = freelook.m_Orbits[i].m_Radius;
            }

            #if UNITY_EDITOR
            SaveDuringPlay.SaveDuringPlay.OnHotSave -= RestoreOriginalOrbits;
            SaveDuringPlay.SaveDuringPlay.OnHotSave += RestoreOriginalOrbits;
            #endif
        }
    }

    void Start()
    {
        newPosition = transform.position;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        SaveDuringPlay.SaveDuringPlay.OnHotSave -= RestoreOriginalOrbits;
    }

    public void AssignTargets(GameObject target)
    {
        freelook.Follow = target.transform;
        freelook.LookAt = target.transform;
    }

    void RestoreOriginalOrbits()
    {
        if (originalOrbits != null)
        {
            for (int i = 0; i < originalOrbits.Length; i++)
            {
                freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height;
                freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius;
            }
        }
    }
#endif

    private void Update()
    {
        //zAxis.m_InputAxisValue = Input.mouseScrollDelta.y / -6;
        zAxis.m_InputAxisValue = -UnityEngine.InputSystem.Mouse.current.scroll.ReadValue().y / 600;

        UpdateOrbit();
    }

    void UpdateOrbit()
    {
        if (originalOrbits != null)
        {
            zAxis.Update(Time.deltaTime);
            float scale = Mathf.Lerp(minScale, maxScale, zAxis.Value);
            for (int i = 0; i < originalOrbits.Length; i++)
            {
                freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * scale;
                freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * scale;
            }
        }

        if (!rotatingCamera)
        {
            //freelook.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X");
            //freelook.m_YAxis.m_InputAxisValue = Input.GetAxis("Mouse Y");

            freelook.m_XAxis.m_InputAxisValue = UnityEngine.InputSystem.Mouse.current.delta.ReadValue().x;
            freelook.m_YAxis.m_InputAxisValue = UnityEngine.InputSystem.Mouse.current.delta.ReadValue().y;

            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else
        {
            freelook.m_XAxis.m_InputAxisValue = 0;
            freelook.m_YAxis.m_InputAxisValue = 0;
        }

        if (movingCamera)
        {
            newPosition += transform.right * (moveInput.x * movementSpeed);
            newPosition += transform.forward * (moveInput.z * movementSpeed);

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        }
        else
        {
            newPosition = transform.position;
        }
    }

    public void OnMove(bool toggle, Vector3 input)
    {
        moveInput = input;
        movingCamera = toggle;
    }

    public void OnRotate(bool toggle)
    {
        rotatingCamera = toggle;
    }
}
