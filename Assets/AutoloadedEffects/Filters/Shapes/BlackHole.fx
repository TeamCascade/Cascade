﻿sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;
float4 uShaderSpecificData;

float uRadius;

float4 PixelShaderFunction(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    // Trying out gravitational lensing.
    // Adapted from https://www.shadertoy.com/view/XdX3DN
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;

    float2 uv = coords.xy / uScreenResolution.xy;
    uv.y = -uv.y;
    
    float2 warpingEffect = normalize(uTargetPosition.xy - coords.xy) * pow(distance(uTargetPosition.xy, coords.xy), -2.0) * 30.0;
    warpingEffect.y = -warpingEffect.y;
    uv = uv + warpingEffect;
    
    float light = clamp(0.1 * distance(uTargetPosition.xy, coords.xy) - 1.5, 0.0, 1.0);
    
    return tex2D(uImage0, uv) * light;
}

technique Technique1
{
    pass AutoloadPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
