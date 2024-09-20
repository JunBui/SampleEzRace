Shader "#NVJOB/NX Shaders/Boids/Bird" {
	Properties {
		[Header(Basic Settings)] [Space(5)] [HDR] _Color ("Main Color", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_Saturation ("Saturation", Range(0, 5)) = 1
		_Brightness ("Brightness", Range(0, 5)) = 1
		_Contrast ("Contrast", Range(0, 5)) = 1
		[Header(Specular Settings)] [Space(5)] _SpecMap ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_SpecMapUV ("Specularmap Uv", Float) = 1
		[HDR] _SpecColor ("Specular Color", Vector) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range(0.03, 2)) = 0.078125
		_SpecMapInts ("Intensity SpecularMap", Range(0, 5)) = 1
		[Header(Occlusion Settings)] [Space(5)] _OcclusionMap ("Occlusionmap", 2D) = "white" {}
		_OcclusionMapUv ("Occlusionmap Uv", Range(0.01, 50)) = 1
		_IntensityOc ("Intensity Occlusion", Range(-20, 20)) = 1
		[Header(Normalmap Settings)] [Space(5)] _BumpMap ("Normalmap", 2D) = "bump" {}
		_IntensityNm ("Intensity Normalmap", Range(-20, 20)) = 1
		_BumpMapD ("Normalmap Detail", 2D) = "bump" {}
		_BumpMapDUV ("Normalmap Detail Uv", Float) = 1
		_IntensityNmD ("Intensity Normalmap Detail", Range(-20, 20)) = 1
		[Header(Reflection Settings)] [Space(5)] [HDR] _ReflectColor ("Reflection Color", Vector) = (1,1,1,0.5)
		_EmissionTex ("Emission (RGB) Gloss (A)", 2D) = "white" {}
		_Cube ("Reflection Cubemap", Cube) = "" {}
		_IntensityRef ("Intensity Reflection", Range(0, 20)) = 1
		_SaturationRef ("Saturation Reflection", Range(0, 5)) = 1
		_ContrastRef ("Contrast Reflection", Range(0, 5)) = 1
		_BiasNormal ("Bias Normal", Range(-5, 5)) = 1
		[Header(Bird Flapping)] [Space(5)] _FlappingSpeed ("Flapping Speed", Range(0, 50)) = 10
		_FlappYPower ("Flapping Y Power", Range(0, 50)) = 2
		_FlappYOffset ("Flapping Y Offset", Range(-15, 15)) = 0.1
		_FlappXPower ("Flapping X Power", Range(0, 50)) = 1
		_FlappXOffset ("Flapping X Offset", Range(-15, 15)) = 0.1
		_FlappXCenter ("Flapping X Center Indent", Range(0, 15)) = 0.1
		_FlappZPower ("Flapping Z Power", Range(-10, 10)) = 0.1
		_WaveY ("Wave Y", Range(0, 30)) = 0
		_WaveYSpeed ("Wave Y Speed", Range(0, 30)) = 1
		[Toggle(BUTTERFLY)] _FillWithRed ("Butterfly", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Legacy Shaders/VertexLit"
}