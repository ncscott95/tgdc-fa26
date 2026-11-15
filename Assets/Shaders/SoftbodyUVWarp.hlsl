int _PointCount;
float4 _RestPoints[32];
float4 _CurrentPoints[32];

void WarpSoftbodyUV_float(float2 ObjectPos, float2 CenterOffset, float Falloff, float UVScale, out float2 WarpedUV)
{
    float2 currentCenter = float2(0.0, 0.0);
    float2 restCenter = float2(0.0, 0.0);
    for (int i = 0; i < _PointCount; i++)
    {
        currentCenter += _CurrentPoints[i].xy;
        restCenter += _RestPoints[i].xy;
    }
    float invCount = 1.0 / max((float)_PointCount, 1.0);
    currentCenter *= invCount;
    restCenter *= invCount;

    float sinSum = 0.0;
    float cosSum = 0.0;
    for (int j = 0; j < _PointCount; j++)
    {
        float2 r = _RestPoints[j].xy - restCenter;
        float2 c = _CurrentPoints[j].xy - currentCenter;
        cosSum += dot(r, c);
        sinSum += (r.x * c.y - r.y * c.x);
    }
    float angle = atan2(sinSum, cosSum);

    float s = sin(-angle);
    float c = cos(-angle);
    float2x2 invRot = float2x2(c, -s, s, c);

    float2 unrotatedPixel = mul(invRot, ObjectPos - currentCenter) + restCenter;

    float2 totalDisplacement = float2(0.0, 0.0);
    float totalWeight = 0.0;

    for (int k = 0; k < _PointCount; k++)
    {
        float2 unrotatedPt = mul(invRot, _CurrentPoints[k].xy - currentCenter) + restCenter;
        float2 restPt = _RestPoints[k].xy;
        float2 delta = unrotatedPt - restPt;

        float dist = distance(unrotatedPixel, unrotatedPt);
        float weight = 1.0 / (pow(dist, Falloff) + 0.001);

        totalDisplacement += delta * weight;
        totalWeight += weight;
    }

    float2 avgDisplacement = (totalWeight > 0.0) ? (totalDisplacement / totalWeight) : float2(0.0, 0.0);

    float2 restSamplePos = unrotatedPixel - avgDisplacement;
    WarpedUV = (restSamplePos * UVScale) + CenterOffset;
}
