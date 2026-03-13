using UnityEngine;

public static class MathFunctions 
{
    // Calculates the degrees of rotation from Vector2.Right as a float
    public static float VectorToDegrees(Vector2 vector)
    {
        //First normalize the vector
        Vector2 v = vector.normalized;
        
        float degrees = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        
        return degrees;
    }

    // Calculates a vector representing the direction, given degrees from Vector2.Right
    public static Vector2 DegreesToVector(float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        
        Vector2 v = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        return v.normalized;
    }
}
