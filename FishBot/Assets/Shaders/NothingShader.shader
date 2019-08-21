Shader "Custom/DrawNothing"
{     
     SubShader
     {
         Lighting Off
         Cull Front   //don't draw polygons facing camera
         Colormask 0 Zwrite Off
         Pass{}
     }
}
