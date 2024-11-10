using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static OpenTK.Windowing.GraphicsLibraryFramework.GLFW;


namespace OpenTk3D.src
{
    public static class Camera
    {

        public static float Yaw = -90f;
        public static float Pitch = 0;
        public static float Roll = 0;
        public static Vector3 Position = Vector3.Zero;
        public static Vector3 Forwards = new Vector3(0, 0, -1);
        public static Vector3 Up = new Vector3(0, 1, 0);
        public static Vector3 Right = new Vector3(1, 0, 0);
        public static float Speed = 20f;

        public unsafe static Matrix4 CalculateViewMatrix(WindowManager windowManager, float MouseAccelerationX, float MouseAccelerationY)
        {
            Speed = 5 * Program.DeltaTime;
            Forwards = Vector3.Normalize(new Vector3((float)Math.Cos(IcantMath.DegreeToRadian(Yaw)) * (float)Math.Cos(IcantMath.DegreeToRadian(Pitch)), (float)Math.Sin(IcantMath.DegreeToRadian(Pitch)), (float)Math.Sin(IcantMath.DegreeToRadian(Yaw)) * (float)Math.Cos(IcantMath.DegreeToRadian(Pitch))));
            Right = Vector3.Normalize(Vector3.Cross(Forwards, new Vector3(0, 1, 0)));
            Up = Vector3.Normalize(Vector3.Cross(Right, Forwards));
            

            if (GetKey(windowManager.Window, Keys.W) == InputAction.Press)
            {
                Position += Speed * Forwards;
            }
            if (GetKey(windowManager.Window, Keys.A) == InputAction.Press)
            {
                Position -= Speed * Right;
            }
            if (GetKey(windowManager.Window, Keys.S) == InputAction.Press)
            {
                Position -= Speed * Forwards;
            }
            if (GetKey(windowManager.Window, Keys.D) == InputAction.Press)
            {
                Position += Speed * Right;
            }
            if (GetKey(windowManager.Window, Keys.Space) == InputAction.Press)
            {
                Position += Speed * new Vector3(0, 1, 0);
            }
            if (GetKey(windowManager.Window, Keys.LeftShift) == InputAction.Press)
            {
                Position -= Speed * new Vector3(0, 1, 0);
            }

            Yaw += MouseAccelerationX;
            Pitch += MouseAccelerationY;

            return Matrix4.LookAt(Position, Position + Forwards, Up);
        }
    }
}
