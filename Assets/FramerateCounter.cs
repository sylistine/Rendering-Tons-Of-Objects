using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FramerateCounter : MonoBehaviour
{
    public Text targetGUIElement;
    private int sampleSize = 100;
    private Queue<float> deltaTimeSamples = new Queue<float>();

    void Update()
    {
        float averageDeltaTime = 0;
        int fps;

        if (deltaTimeSamples.Count >= sampleSize) deltaTimeSamples.Dequeue();
        deltaTimeSamples.Enqueue(Time.deltaTime);
        
        for(int i = 0; i < sampleSize; i++)
        {
            float tmp = deltaTimeSamples.Dequeue();
            averageDeltaTime += tmp;
            deltaTimeSamples.Enqueue(tmp);
        }

        averageDeltaTime = averageDeltaTime / sampleSize;
        fps = (int)(1 / averageDeltaTime);

        targetGUIElement.text = fps + " fps";
    }
}
