using Academy.HoloToolkit.Unity;
using UnityEngine;

/// <summary>
/// GestureAction performs custom actions based on 
/// which gesture is being performed.
/// </summary>
public class GestureActionNote : MonoBehaviour
{
    public float translationSensitivity;

    private Vector3 manipulationPreviousPosition;

    void Update()
    {
    }

    void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        if (GestureManager.Instance.IsManipulating && !TransformManager.Instance.isSlicing
            && !TransformManager.Instance.isMoving && !TransformManager.Instance.isScaling)
        {
            Vector3 moveVector = Vector3.zero;
            // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationPreviousPosition;
                        
            // 4.a: Increment this transform's position by the moveVector.
            this.transform.position += translationSensitivity * moveVector;
            
            // 4.a: Update the manipulationPreviousPosition with the current position.
            manipulationPreviousPosition = position;

            //transform.LookAt(Camera.main.transform);
        }
    }
}