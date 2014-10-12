using UnityEngine;
using System.Collections;

public static class InputHelper
{
    public static float ClampAngle(float angle, float min, float max)
    {
        //Keeps the rotation between -360 and 360 degrees
        do
        {
            if (angle < -360)
            {
                angle += 360;
            }

            if (angle > 360)
            {
                angle -= 360;
            }

        } while (angle < -360 || angle > 360);

        float finalAngle = Mathf.Clamp(angle, min, max);

        return finalAngle;
    }
}
