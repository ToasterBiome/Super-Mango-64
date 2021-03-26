Shader "TreeWind/Wind_Main" {



    Properties{

        _MainTex("Main Texture", 2D) = "white" {}
        _BumpMap("Normalmap", 2D) = "bump" {}
        _Color("Color", Color) = (1,1,1,1)
        _AlphaCutoff("Alpha Cutoff",range(0,1)) = 0.03

        _wind_dir("Wind Direction", Vector) = (0.5,0.05,0.5,0)


        _tree_sway("Tree Sway Offset", range(0,1)) = 0.2
        _tree_sway_disp("Tree Sway Displacement", range(0,1)) = 0.3
        _tree_sway_speed("Tree Sway Speed", range(0,10)) = 1
        _wind_size("Wind Wave Size", range(50,5)) = 15

    }



        SubShader{
            Tags {
                //"Queue"="AlphaTest"
                "RenderType" = "Opaque"
            }
            LOD 400
            cull off

            CGPROGRAM
            #include "TerrainEngine.cginc"
            #pragma target 5.0

            #pragma surface surf Lambert vertex:vert addshadow fullforwardshadows 



            //Declared Variables

            float4 _wind_dir;
            float _wind_size;
            float _tree_sway_speed;
            float _tree_sway_disp;
            float _tree_sway;
            fixed _AlphaCutoff;


            sampler2D _BumpMap;
            sampler2D _MainTex;

            fixed4 _Color;



            //Structs

            struct Input {

                float2 uv_MainTex;
                float2 uv_BumpMap;
                float3 viewDir;


            };



            // Vertex Manipulation Function

            void vert(inout appdata_full i) {


                //Gets the vertex's World Position 

               float3 worldPos = mul(unity_ObjectToWorld, i.vertex).xyz;


               //Tree Movement and Wiggle

               i.vertex.x += (cos(_Time.z * _tree_sway_speed + (worldPos.x / _wind_size) + (sin(_Time.z * _tree_sway * _tree_sway_speed + (worldPos.x / _wind_size)) * _tree_sway)) + 1) / 2 * _tree_sway_disp * _wind_dir.x * (i.vertex.y / 10) +
               cos(_Time.w * i.vertex.x * _tree_sway + (worldPos.x / _wind_size)) * _tree_sway * _wind_dir.x * i.color.b;

               i.vertex.z += (cos(_Time.z * _tree_sway_speed + (worldPos.z / _wind_size) + (sin(_Time.z * _tree_sway * _tree_sway_speed + (worldPos.z / _wind_size)) * _tree_sway)) + 1) / 2 * _tree_sway_disp * _wind_dir.z * (i.vertex.y / 10) +
               cos(_Time.w * i.vertex.z * _tree_sway + (worldPos.x / _wind_size)) * _tree_sway * _wind_dir.z * i.color.b;

               i.vertex.y += cos(_Time.z * _tree_sway_speed + (worldPos.z / _wind_size)) * _tree_sway_disp * _wind_dir.y * (i.vertex.y / 10);



               //Branches Movement

               i.vertex.y += sin(_Time.w * _tree_sway_speed + _wind_dir.x + (worldPos.z / _wind_size)) * _tree_sway * i.color.r;


           }

            // Surface Shader

            void surf(Input IN, inout SurfaceOutput o) {

                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

                o.Albedo = c.rgb;
                o.Alpha = c.a;
                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
                clip(tex2D(_MainTex, IN.uv_MainTex).a * c.a - _AlphaCutoff);


            }
    ENDCG
        }
            Fallback "Legacy Shaders/Bumped Diffuse"
}