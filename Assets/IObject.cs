using OpenTK.Mathematics;
public interface IObject
{
    float[] vertecies { get; set; }
    
    uint[] indecies { get; set; }
    
    Matrix4 ModelMatrix { get; set; }

    string id { get; set; }

}
