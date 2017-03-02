using Academy.HoloToolkit.Unity;
using UnityEngine;

/// <summary>
/// GestureAction performs custom actions based on 
/// which gesture is being performed.
/// </summary>
public class GestureAction : MonoBehaviour
{
    [Tooltip("Rotation max speed controls amount of rotation.")]
    public float RotationSensitivity = 10.0f;
    public float TranslationSensitivity = 1.5f;
    public float ScalingSensitivity = 2.0f;
    public float SlicingSensitivity = 3.0f;

    private Vector3 manipulationPreviousPosition;
    
    private float xRotationFactor, yRotationFactor, zRotationFactor;

    void Update()
    {
        PerformRotation();
    }

    private void PerformRotation()
    {
        if (GestureManager.Instance.IsNavigating &&
            (!ExpandModel.Instance.IsModelExpanded ||
            (ExpandModel.Instance.IsModelExpanded && HandsManager.Instance.FocusedGameObject == gameObject)))
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.c */
            /*
            // 2.c: Calculate rotationFactor based on GestureManager's NavigationPosition.X and multiply by RotationSensitivity.
            // This will help control the amount of rotation.
            rotationFactor = GestureManager.Instance.NavigationPosition.x * RotationSensitivity;

            // 2.c: transform.Rotate along the Y axis using rotationFactor.
            transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
               */
            xRotationFactor = GestureManager.Instance.NavigationPosition.x * RotationSensitivity;
            yRotationFactor = GestureManager.Instance.NavigationPosition.y * RotationSensitivity;
            zRotationFactor = GestureManager.Instance.NavigationPosition.z * RotationSensitivity;
            // 2.c: transform.Rotate along the Y axis using rotationFactor.
            transform.Rotate(new Vector3(1 * yRotationFactor, 1 * xRotationFactor, 1 * zRotationFactor));
        }
    }

    void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        if (GestureManager.Instance.IsManipulating)
        {
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            Vector3 moveVector = Vector3.zero;
            // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationPreviousPosition;

            if (TransformManager.Instance.isSlicing) // Slice the MRI vertically
            {               
                float val = this.gameObject.GetComponentInChildren<Renderer>().material.GetFloat("_boxMaxY");
                float sliceVal = Mathf.Clamp(val + moveVector.y, -0.5f, 0.5f);
                this.gameObject.GetComponentInChildren<Renderer>().material.SetFloat("_boxMaxY", sliceVal);
            }
            else if (!TransformManager.Instance.isScaling) // Move the hologram.
            {
                // 4.a: Increment this transform's position by the moveVector.
                transform.position += TranslationSensitivity * moveVector;
            }
            else // Scale the hologram.
            {
                if ((position - this.gameObject.transform.position).magnitude > (manipulationPreviousPosition - this.gameObject.transform.position).magnitude)
                {
                    transform.localScale += ScalingSensitivity * (new Vector3(moveVector.magnitude, moveVector.magnitude, moveVector.magnitude));
                }
                else
                {
                    transform.localScale += -ScalingSensitivity * (new Vector3(moveVector.magnitude, moveVector.magnitude, moveVector.magnitude));
                }
                
            }
            // 4.a: Update the manipulationPreviousPosition with the current position.
            manipulationPreviousPosition = position;
        }
    }
}