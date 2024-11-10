using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTk3D.Assets
{
    public class Qube : IObject
    {
        public float[] vertecies { get; set; } =
        {// X   |   Y  | Z| Q| S                  Index
            -0.5f, -0.5f, -0.5f, 0, 0,  //unten links : 0
            0.5f , -0.5f, -0.5f, 1, 0,  //unten rechts: 1
            -0.5f, 0.5f , -0.5f, 0, 1,  //oben  links : 2
            0.5f , 0.5f , -0.5f, 1, 1,   //oben rechts : 3

            // X   |   Y  | Z| Q| S                  Index
            -0.5f, -0.5f, 0.5f, 0, 0,  //unten links : 4
            0.5f , -0.5f, 0.5f, 1, 0,  //unten rechts: 5
            -0.5f, 0.5f , 0.5f, 0, 1,  //oben  links : 6
            0.5f , 0.5f , 0.5f, 1, 1,   //oben rechts :7

            // X   |   Y  | Z| Q| S                  Index
            0.5f, -0.5f,    0.5f,  0, 0,  //unten links : 8
            0.5f, -0.5f,  -0.5f,   1, 0,  //unten rechts: 9
            0.5f, 0.5f ,   0.5f,   0, 1,  //oben  links : 10
            0.5f , 0.5f, -0.5f,    1, 1,   //oben rechts :11

            // X   |   Y  | Z| Q| S                  Index
            -0.5f, -0.5f,  0.5f,  0, 0,  //unten links : 12
            -0.5f, -0.5f, -0.5f,  1, 0,  //unten rechts: 13
            -0.5f, 0.5f ,  0.5f,  0, 1,  //oben  links : 14
            -0.5f , 0.5f, -0.5f,  1, 1,   //oben rechts : 15

            // X   |   Y  | Z| Q| S                  Index
            -0.5f, 0.5f, -0.5f, 0, 0,  //unten links : 16
            0.5f , 0.5f, -0.5f, 1, 0,  //unten rechts: 17
            -0.5f, 0.5f , 0.5f, 0, 1,  //oben  links : 18
            0.5f , 0.5f , 0.5f, 1, 1,   //oben rechts: 19

            // X   |   Y  | Z| Q| S                  Index
            -0.5f, -0.5f, -0.5f, 0, 0,  //unten links : 20
            0.5f , -0.5f, -0.5f, 1, 0,  //unten rechts: 21
            -0.5f, -0.5f , 0.5f, 0, 1,  //oben  links : 22
            0.5f , -0.5f , 0.5f, 1, 1,  //oben rechts : 23
        };

        public uint[] indecies { get; set; } =
        {
            1, 0, 2,
            2, 3, 1,

            5, 4, 6,
            6, 7, 5,

            9, 8, 10,
            10, 11, 9,

            13, 12, 14,
            14, 15, 13,

            17, 16, 18,
            18, 19, 17,

            21, 20, 22,
            22, 23, 21
        };

        public Matrix4 ModelMatrix { get; set; }
        public string id { get; set; }

        public Qube (Vector3 position, float RotationX, float RotationY, float RotationZ, string id)
        {
            ModelMatrix = Matrix4.CreateRotationX(IcantMath.DegreeToRadian(RotationX));
            ModelMatrix *= Matrix4.CreateRotationY(IcantMath.DegreeToRadian(RotationY));
            ModelMatrix *= Matrix4.CreateRotationZ(IcantMath.DegreeToRadian(RotationZ));
            ModelMatrix *= Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            this.id = id;
        }
    }
}
