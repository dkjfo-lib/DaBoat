#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void CalculateMainLight_float(float3 WorldPos, out float3 Direction, out float3 Color)
{
    Light mainLight = GetMainLight();
    Direction = mainLight.direction;
    Color = mainLight.color;
}

#endif