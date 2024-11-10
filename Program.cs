using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static OpenTK.Windowing.GraphicsLibraryFramework.GLFW;
using static OpenTK.Graphics.OpenGL.GL;
using OpenTk3D.src;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using OpenTK.Graphics.ES11;
using StbImageSharp;
using OpenTk3D;
using OpenTK.Windowing.Common;
using System.Diagnostics;
using OpenTk3D.Assets;

public static unsafe class Program
{
    public static float DeltaTime = 0f;

    public static string Message = String.Empty;
    public static WindowManager windowManager;
    public static List<IObject> objects = new List<IObject>();
    

    static void ListenConsole()
    {
        while (true)
        {
            Message = Console.ReadLine();
        }
    }

    static unsafe void Main(string[] args)
    {
        Console.WriteLine("To create Qubes, enter following command into the console: Spawn(X-Position, Y_Position, Z_Position, X-Rotation, Y-Rotation, Z-Rotation)");
        Console.WriteLine("To Move use W A S D");
        Console.WriteLine("To Move up use Space");
        Console.WriteLine("To Move down use left Shift");
        Console.WriteLine("To look around use your mouse");
        Console.WriteLine("Have fun!!!");

        Thread ConsoleListener = new Thread(ListenConsole);
        ConsoleListener.Start();

        int windowWidth = 600;
        int windowHeight = 600;

        windowManager = new WindowManager(windowWidth, windowHeight, "OpenTk3D");
        GLFW.SetInputMode(windowManager.Window, CursorStateAttribute.Cursor, CursorModeValue.CursorHidden);

        Vector3 Playerposition = Vector3.Zero;
        Vector3 ModelPosition = new Vector3(0, 0, -3);
        float MouseSensetivity = 0.05f;
        Stopwatch DeltaTime = new Stopwatch();

        GLFW.SetWindowSizeCallback(windowManager.Window, new GLFWCallbacks.WindowSizeCallback(new GLFWCallbacks.WindowSizeCallback(Program.ResizeCallback)));

        float[] vertecies = 
        { // X   |   Y  | Z| Q| S                  Index
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
        uint[] indecies = { 1, 0, 2,
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
                            22, 23, 21};

        Enable(OpenTK.Graphics.OpenGL.EnableCap.DepthTest);


        VertexArray vao = new VertexArray();
        vao.Bind();
        VertexBuffer vbo = new VertexBuffer(vertecies);
        vao.PushFloatToAttributes(3);
        vao.PushFloatToAttributes(2);
        vao.BindBuffer(vbo);
        vao.Bind();
        vbo.Bind();

        IndexBuffer ibo = new IndexBuffer(indecies);
        ibo.Bind();
        Shader shader = new Shader("C:\\Users\\Admin\\source\\repos\\OpenTk3D\\OpenTk3D\\Shader\\Default.ver", "C:\\Users\\Admin\\source\\repos\\OpenTk3D\\OpenTk3D\\Shader\\Default.frag");

        Texture texture = new Texture("C:\\Users\\Admin\\source\\repos\\OpenTk3D\\OpenTk3D\\Sprites\\wall.jpg");
        texture.Bind(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
        
        Matrix4 model = Matrix4.CreateRotationY(0);
        model *= Matrix4.CreateRotationX(IcantMath.DegreeToRadian(0));
        model *= Matrix4.CreateTranslation(ModelPosition);
        Matrix4 view = Matrix4.Identity;
        Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(1.5f, windowWidth / windowHeight,0.1f, 100);

        
        shader.SetMatrixUniform("model", model);
        shader.SetMatrixUniform("view", view);
        shader.SetMatrixUniform("projection", proj);


        objects.Add(new Qube(new Vector3(0, 0, -3), 0, 0, 0, "Qube 1"));
        objects.Add(new Qube(new Vector3(0, 0, -5), 0, 0, 0, "Qube 2"));

        while (!GLFW.WindowShouldClose(windowManager.Window))
        {
            //Update:
            if (Message != String.Empty)
            {
                if(Message.Contains("Spawn"))
                {
                    Message = Message.Replace("Spawn", "");
                    Message = Message.Replace("(", string.Empty);
                    Message = Message.Replace(" ", string.Empty);
                }
                int x = 0;
                int y = 0;
                int z = 0;
                int rotationx = 0;
                int rotationy = 0;
                int rotationz = 0;
                int count = 1;
                StringBuilder current = new StringBuilder();

                foreach(char c in Message)
                {
                    if(c == ',' || c == ')')
                    {
                        switch(count)
                        {
                            case 1:
                                x = Int32.Parse(current.ToString());
                                break;
                            
                            case 2:
                                y = Int32.Parse(current.ToString());
                                break;
                            
                            case 3:
                                z = Int32.Parse(current.ToString());
                                break;
                            
                            case 4:
                                rotationx = Int32.Parse(current.ToString());
                                break;
                            
                            case 5:
                                rotationy = Int32.Parse(current.ToString());
                                break;
                            
                            case 6:
                                rotationz = Int32.Parse(current.ToString());
                                break;
                        }
                        current.Clear();
                        count++;
                    }else
                    {
                        current.Append(c);
                    }
                }
                Console.WriteLine(x);
                Console.WriteLine(y);
                Console.WriteLine(z);
                Console.WriteLine(rotationx);
                Console.WriteLine(rotationy);
                Console.WriteLine(rotationz);
                Console.WriteLine("---------");
                Message = String.Empty;
                objects.Add(new Qube(new Vector3(x, y , z), rotationx, rotationy, rotationz, $"{objects.Count - 1}"));
            }

            GLFW.GetWindowSize(windowManager.Window, out windowWidth, out windowHeight);

            DeltaTime.Start();
            ClearColor(OpenTK.Mathematics.Color4.DarkOliveGreen);
            Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit);
            
            Viewport(0, 0, windowWidth, windowHeight);
            view = Camera.CalculateViewMatrix(windowManager, InputManager.GetMouseAcceleration().X * MouseSensetivity, InputManager.GetMouseAcceleration().Y * MouseSensetivity);
            proj = Matrix4.CreatePerspectiveFieldOfView(1.5f, (float)Math.Max(windowWidth, windowHeight) / Math.Min(windowWidth, windowHeight), 0.1f, 100);

            //set uniform
            shader.SetMatrixUniform("projection", proj);
            shader.SetMatrixUniform("view", view);
            
            //Rendering multiple Objects
            foreach(IObject Object in objects)
            {
                shader.SetMatrixUniform("model", Object.ModelMatrix);

                vao.Bind();
                vbo = new VertexBuffer(vertecies);
                vao.BindBuffer(vbo);
                
                vao.Bind();
                vbo.Bind();

                DrawElements(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles, indecies.Length, OpenTK.Graphics.OpenGL.DrawElementsType.UnsignedInt, 0);
            }
            InputManager.SaveLasteMousePosition();
            SwapBuffers(windowManager.Window);
            PollEvents();
            Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);
            DeltaTime.Stop();
            Program.DeltaTime = (float)DeltaTime.ElapsedMilliseconds / 1000;
            DeltaTime.Restart();
        }
        

        Terminate();

    }
    
    public static void ResizeCallback(Window* window, int newWidth, int newHeight)
    {
        Viewport(0,0, newWidth, newHeight);
    }
}