using UnityEngine;

public class Tilt
{
    private float velMagFor45Degree = 15f;
    private float initRotX;
    private float maxTilt;

    public Tilt(float initRotX, float maxTilt)
    {
        this.initRotX = initRotX;
        this.maxTilt = maxTilt;
    }
    public Quaternion getTilt(Rigidbody rb)
    {
        Quaternion tiltAngle = TiltRotationTowardsVelocity(rb.rotation, Vector3.up, rb.velocity, velMagFor45Degree);
        Quaternion newAngle = tiltAngle;
        if (tiltAngle.eulerAngles.x >= initRotX + maxTilt)
        {
            newAngle = Quaternion.Euler(initRotX + maxTilt, tiltAngle.eulerAngles.y, tiltAngle.eulerAngles.z);
        }
        else if (tiltAngle.eulerAngles.x <= initRotX - maxTilt)
        {
            newAngle = Quaternion.Euler(initRotX - maxTilt, tiltAngle.eulerAngles.y, tiltAngle.eulerAngles.z);
        }

        return newAngle;
    }

    /// <summary>
    /// Tilts a rotation towards a velocity relative to referenceUp
    /// Example:
    /// myTransform.rotation = TiltRotationTowardsVelocity( myCleanRotation.rotation, Vector3.up, velocity, 20F );
    /// </summary>
    /// <param name="cleanRotation"        >Target rotation of the transform, maybe your transform is already looking at something, you don't want to loose this alignment</param>
    /// <param name="referenceUp"        >The up Vector, mostly, this will be Vector3.up, if your gravity is pointing down</param>
    /// <param name="vel"                >The velocity vector that is meant to cause the tilt</param>
    /// <param name="velMagFor45Degree"    >A velocity with a magnitude of velMagFor45Degree will yield a 45degree tilt</param>
    /// <returns>returns currentRotation modified by a tilt</returns>
    private static Quaternion TiltRotationTowardsVelocity(Quaternion cleanRotation, Vector3 referenceUp, Vector3 vel, float velMagFor45Degree)
    {
        Vector3 rotAxis = Vector3.Cross(referenceUp, vel);
        float tiltAngle = Mathf.Atan(vel.magnitude / velMagFor45Degree) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(tiltAngle, rotAxis) * cleanRotation;    //order matters
    }
}
