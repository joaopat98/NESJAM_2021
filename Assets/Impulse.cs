using UnityEngine;

public class Impulse
{
    Vector2 direction;
    float time;
    float currentTime;
    float distance;
    public bool isFinished { get => currentTime >= time; }

    public Impulse(Vector2 Offset, float time)
    {
        distance = Offset.magnitude;
        direction = Offset / distance;
        this.time = time;
    }

    public void Update(float timeStep)
    {
        currentTime += timeStep;
    }

    public Vector2 GetCurrentOffset(float timeStep)
    {
        float v = Mathf.Lerp(2 * distance / time, 0, currentTime / time);
        return direction * v * timeStep;
    }
}