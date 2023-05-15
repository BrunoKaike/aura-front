using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;            
    public float distance = 5.0f;       
    public float height = 2.0f;         
    public float smoothTime = 0.2f;     // Tempo de suavização da câmera
    public float rotationSpeed = 5.0f;  // Velocidade de rotação da câmera

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private Quaternion targetRotation;

    private void Start()
    {
        offset = new Vector3(0, height, -distance);
        targetRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;

        // Calcula a rotação desejada da câmera
        Vector3 lookDirection = target.position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(lookDirection, target.up);

        // Suaviza a rotação da câmera usando Slerp
        targetRotation = Quaternion.Slerp(targetRotation, desiredRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
}
