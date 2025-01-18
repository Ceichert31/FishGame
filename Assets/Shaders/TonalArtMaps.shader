Shader "Unlit/TonalArtMaps"
{
    Properties
    {
        _TonalArtMap1 ("Texture", 2D) = "white" {}
        _TonalArtMap2 ("Texture", 2D) = "white" {}
        _TAM2DArray ("2D Array", 2DArray) = "" {} 
        _Tiling ("Tiling", Vector) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Tags { "LightMode"="UniversalForwardOnly" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                UNITY_FOG_COORDS(1)
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 diff : COLOR0; //Diffuse lighting color
            };

            sampler2D _TonalArtMap1;
            float4 _TonalArtMap1_ST;
            UNITY_DECLARE_TEX2DARRAY(_TAM2DArray);
            float4 _Tiling;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _TonalArtMap1);

                //Get world space vertex normal
                half3 worldNormal = UnityObjectToWorldNormal(v.vertex);

                //Get the dot product between the vertex's world normal and the light position
                o.diff = max(0.0, dot(worldNormal, _WorldSpaceLightPos0.xyz)) * _LightColor0;

                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            //Returns the average color of a pixel
            float PixelBrightness(float3 pixelColor)
            {
                return pixelColor.r + pixelColor.g + pixelColor.b / 3.0;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //Get the brightness value of diffuse lighting
                fixed lum = Luminance(i.diff);

                //Invert the brightness value
                lum = (1 - lum);
                
                fixed4 tonalArtMap = UNITY_SAMPLE_TEX2DARRAY(_TAM2DArray, float3(i.uv * _Tiling, lum));
                
                //Get texture color
                fixed4 col = tex2D(_TonalArtMap1, i.uv);

                //Apply diffuse lighting
                col = i.diff * Luminance(col) * tonalArtMap;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
