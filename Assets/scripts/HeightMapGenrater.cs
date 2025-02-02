using UnityEngine;

public class HeightMapGenrater 
{
    public static float GenrateHeightMap(float xScale, float yScale, int numOfOctaves, float persistance, float lacunarity, int seed) {
        float totalNoise = 0;
        float maxAmplitude = 0;

        for(int i = 0; i < numOfOctaves; i++) {
            float frequency = Mathf.Pow(lacunarity, i); // lacunarity is increase in frequency with each octave
            float amplitude = Mathf.Pow(persistance, i); // pesistence is decrease in amplitude with each octave

            float xNoise = xScale * frequency + seed * 0.1f;
            float yNoise = yScale * frequency + seed * 0.1f;

            
            totalNoise += Mathf.PerlinNoise(xNoise, yNoise) * amplitude;
            maxAmplitude += amplitude;
        }
        return totalNoise / maxAmplitude; // normalize to range 0 to 1
    }
}
