Shader "Custom/StencilBufferTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [IntRange] _StencilID ("Stencil ID", Range(0, 255)) = 0
    }
    SubShader
    {
        Tags 
        { 
        "RenderType" = "Opaque" 
        "RenderPipeline" = "UniversalPipeline"
        "Queue" = "Geometry"
        }

        pass
        {
            Blend Zero One
            ZWrite Off

            Stencil
            {
                //Stencil Value
                Ref [_StencilID]

                //Stencil Test always passes
                Comp Always

                //Replace old value with our value
                Pass Replace
                
                //If test fails, keep old value
                Fail Keep
            }
        }
    }
}